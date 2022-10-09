using Microsoft.Extensions.Options;
using OOMetrics.Abstractions.Interfaces;

namespace OOMetrics.Metrics
{
    public class MetricsCalculator
    {
        private readonly IDeclarationProvider declarationProvider;
        private readonly MetricsCalculatorOptions options;
        private IEnumerable<string> allowedPackages;
        private IEnumerable<string> internalPackages;
        private readonly List<Package> packages = new List<Package>();
        public List<Package> Packages { get { return packages.Where(d => allowedPackages.Contains(d.Name)).ToList(); }  }
        public MetricsCalculator(IDeclarationProvider declarationProvider, IOptions<MetricsCalculatorOptions> options)
        {
            this.declarationProvider = declarationProvider;
            this.options = options.Value;
        }
        public async Task AnalyzeData()
        {
            var declarations = await declarationProvider.GetDeclarations();
            internalPackages = declarations.Select(d => d.ContainingPackage).Distinct();
            allowedPackages = (!options.PackagesToAnalyze.Any()) ? internalPackages : options.PackagesToAnalyze;
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
                        referencedPackage.AddIncomingDependency(declaration);
                    }
                }
            }
        }
        private bool CheckIfAdd(IDeclaration dependency, string containingPackage)
        {
            var isFromTheSamePackage = dependency.ContainingPackage == containingPackage;
            var isFromInternalNamespace = internalPackages.Contains(dependency.ContainingPackage);
            var returnValue = (isFromInternalNamespace || options.IncludeExtrenalDependencies) && !isFromTheSamePackage;
            return returnValue;
        }
        private bool CheckIfAddIncomingDependency(string declarationPackage, string dependencyPackage)
        {
            var isAllowed = allowedPackages.Contains(dependencyPackage);
            var isFromTests = declarationPackage == string.Format(options.TestProjectNamePattern, dependencyPackage);            
            var excludedTestDependency = options.ExcludeIncomingDependenciesFromTests && isFromTests;
            return isAllowed && !(excludedTestDependency);
        }
        private Package RegisterPackage(string packageName)
        {
            var package = packages.Where(p => p.Name == packageName).FirstOrDefault();
            if (package is null)
            {
                package = new Package(packageName);
                packages.Add(package);
            }
            return package;
        }
    }
}