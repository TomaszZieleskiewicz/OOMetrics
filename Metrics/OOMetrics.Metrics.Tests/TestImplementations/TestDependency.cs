using OOMetrics.Abstractions.Abstract;
using OOMetrics.Abstractions.Interfaces;

namespace OOMetrics.Metrics.Tests.TestImplementations
{
    public class TestDependency : ComparableByStringHash, IDependency
    {
        public string Name { get; }
        public string DependencyNamespace { get; }
        public string ContainingPackage { get; }
        public TestDependency(string name, string dependencyNamespace, string containingPackage)
        {
            Name = name;
            DependencyNamespace = dependencyNamespace;
            ContainingPackage = containingPackage;
        }
        public override string ToString()
        {
            return $"{Name} in {DependencyNamespace} from {ContainingPackage}";
        }
    }
}
