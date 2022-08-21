using OOMetrics.Metrics.Interfaces;

namespace OOMetrics.Metrics.Models
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
        public override bool Equals(object? obj)
        {
            if (obj?.GetType() == typeof(Dependency))
            {
                return GetHashCode() == ((Dependency)obj).GetHashCode();
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        public override string ToString()
        {
            return $"{Name}-{DependencyNamespace}-{ContainingAssembly}";
        }
    }
}
