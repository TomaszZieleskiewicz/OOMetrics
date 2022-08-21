using OOMetrics.Abstractions;

namespace OOMertics.Helper.Implementations
{
    public class Dependency : IDependency
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
        public override bool Equals(object? obj)
        {
            return (obj is null) ? base.Equals(obj) : GetHashCode() == obj.GetHashCode();
        }
        public override int GetHashCode()
        {
            var stringRepresentation = ToString();
            if (stringRepresentation == null)
            {
                return 0;
            }
            return stringRepresentation.GetHashCode();
        }
        public override string ToString()
        {
            return $"{Name} in {DependencyNamespace} from {ContainingPackage}";
        }
    }
}
