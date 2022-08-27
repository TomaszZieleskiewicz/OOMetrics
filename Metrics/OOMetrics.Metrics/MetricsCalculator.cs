using OOMetrics.Abstractions.Interfaces;

namespace OOMetrics.Metrics
{
    public class MetricsCalculator
    {
        private readonly List<IDeclaration> RawData;
        private readonly MetricsCalculatorOptions Options = new MetricsCalculatorOptions();
        public readonly List<Package> Packages = new List<Package>();
        public MetricsCalculator(List<IDeclaration> data, MetricsCalculatorOptions options)
        {
            RawData = data;
            Options = options;
        }
        public MetricsCalculator(List<IDeclaration> data)
        {
            RawData = data;
        }
        public void AnalyzeData()
        {
            foreach (var declaration in RawData)
            {
                var package = RegisterPackage(declaration.ContainingPackage);
                package.AddDeclaration(declaration);

                foreach (var dependency in declaration.Dependencies.Where(d => CheckIfAdd(d, declaration.ContainingPackage)))
                {
                    package.AddOutgoingDependency(dependency);
                    if(CheckIfAddIncomingDependency(declaration.ContainingPackage,dependency.ContainingPackage))
                    {
                        var referencedPackage = RegisterPackage(dependency.ContainingPackage);
                        referencedPackage.AddIncomingDependency(declaration.ToDependency());
                    }
                }
            }
            bool CheckIfAdd(IDependency dependency, string containingPackage)
            {
                var isFromTheSamePackage = dependency.ContainingPackage == containingPackage;
                var inIgnoredNamespace = Options.IgnoredDependencyNamespaces.Contains(dependency.DependencyNamespace);
                var returnValue = !(isFromTheSamePackage || inIgnoredNamespace);
                return returnValue;
            }
            bool CheckIfAddIncomingDependency(string declarationPackage, string dependencyPackage)
            {
                var excludedTestDependency = (Options.ExcludeIncomingDependenciesFromTests && declarationPackage == $"{dependencyPackage}.Tests");
                var excludedIncoming = Options.IgnoredIncomingDependencyNamespaces.Contains(declarationPackage);
                return !(excludedTestDependency || excludedIncoming);
            }
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