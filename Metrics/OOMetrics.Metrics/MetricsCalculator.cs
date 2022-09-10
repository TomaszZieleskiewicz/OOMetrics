using Microsoft.Extensions.Options;
using OOMetrics.Abstractions.Interfaces;

namespace OOMetrics.Metrics
{
    public class MetricsCalculator
    {
        private readonly IDeclarationProvider declarationProvider;
        private readonly IMetricsCalculatorOptions Options;
        public readonly List<Package> Packages = new List<Package>();
        public MetricsCalculator(IDeclarationProvider declarationProvider, IOptions<IMetricsCalculatorOptions> options)
        {
            this.declarationProvider = declarationProvider;
            Options = options.Value;
        }
        public async void AnalyzeData()
        {
            var declarations = await declarationProvider.GetDeclarations();
            foreach (var declaration in declarations)
            {
                var package = RegisterPackage(declaration.ContainingPackage);
                package.AddDeclaration(declaration);

                foreach (var dependency in declaration.Dependencies.Where(d => CheckIfAdd(d, declaration.ContainingPackage)))
                {
                    package.AddOutgoingDependency(dependency);
                    if (CheckIfAddIncomingDependency(declaration.ContainingPackage, dependency.ContainingPackage))
                    {
                        var referencedPackage = RegisterPackage(dependency.ContainingPackage);
                        referencedPackage.AddIncomingDependency(declaration.ToDependency());
                    }
                }
            }
        }
        private bool CheckIfAdd(IDependency dependency, string containingPackage)
        {
            var isFromTheSamePackage = dependency.ContainingPackage == containingPackage;
            var inIgnoredNamespace = Options.IgnoredDependencyNamespaces.Contains(dependency.DependencyNamespace);
            var returnValue = !(isFromTheSamePackage || inIgnoredNamespace);
            return returnValue;
        }
        private bool CheckIfAddIncomingDependency(string declarationPackage, string dependencyPackage)
        {
            var excludedTestDependency = (Options.ExcludeIncomingDependenciesFromTests && declarationPackage == $"{dependencyPackage}.Tests");
            var excludedIncoming = Options.IgnoredIncomingDependencyNamespaces.Contains(declarationPackage);
            return !(excludedTestDependency || excludedIncoming);
        }
        private Package RegisterPackage(string packageName)
        {
            var package = Packages.Where(p => p.Name == packageName).FirstOrDefault();
            if (package is null)
            {
                package = new Package(packageName);
                Packages.Add(package);
            }
            return package;
        }
    }
}