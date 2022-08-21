using OOMetrics.Abstractions;

namespace OOMertics.Helper.Implementations
{
    public class Dependency : ComparableByStringHash, IDependency
    {
        public string Name { get; }
        public string DependencyNamespace { get; }
        public string ContainingPackage { get; }
        public Dependency(string name, string dependencyNamespace, string containingPackage)
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