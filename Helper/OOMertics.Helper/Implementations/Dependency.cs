using OOMetrics.Metrics.Interfaces;

namespace OOMertics.Helper.Implementations
{
    public class Dependency : IDependency
    {
        public string Name { get; }
        public string DependencyNamespace { get; }
        public string ContainingAssembly { get; }
        public Dependency(string name, string dependencyNamespace, string containingAssembly)
        {
            Name = name;
            DependencyNamespace = dependencyNamespace;
            ContainingAssembly = containingAssembly;
        }
        public override string ToString()
        {
            return $"{Name} in {DependencyNamespace} from {ContainingAssembly}";
        }
    }
}
