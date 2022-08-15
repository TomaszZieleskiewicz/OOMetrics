using OOMetrics.Metrics.Interfaces;

namespace OOMertics.Helper.Implementations
{
    public class Dependency : IDependency
    {
        public string Name { get; }
        public string Namespace { get; }
        public Dependency(string name, string dependencyNamespace)
        {
            Name = name;
            Namespace = dependencyNamespace; 
        }
        public override string ToString()
        {
            return $"{Name}, {Namespace}";
        }
    }
}
