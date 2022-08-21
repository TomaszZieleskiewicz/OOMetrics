using OOMetrics.Abstractions;

namespace OOMetrics.Metrics
{
    public class MetricsCalculator
    {
        private readonly List<IDeclaration> RawData;
        public readonly List<Package> Packages = new List<Package>();
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

                foreach (var dependency in declaration.Dependencies.Where(d => d.ContainingPackage != declaration.ContainingPackage))
                {
                    package.AddOutgoingDependency(dependency);
                    var referencedPackage = RegisterPackage(dependency.ContainingPackage);
                    referencedPackage.AddIncomingDependency(declaration.ToDependency());
                }
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
