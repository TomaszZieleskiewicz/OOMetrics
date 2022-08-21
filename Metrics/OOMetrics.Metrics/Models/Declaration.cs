using OOMertics.Helper;
using OOMetrics.Metrics.Interfaces;

namespace OOMetrics.Metrics.Models
{
    public class Declaration : IDeclaration
    {
        public string Name { get; }
        public DeclarationType Type { get; }
        public string DeclarationNamespace { get; }
        public ICollection<IDependency> Dependencies { get; }
        public string ContainingAssembly { get; }

        public bool IsAbstract => Type == DeclarationType.INTERFACE_TYPE || Type == DeclarationType.ABSTRACT_CLASS_TYPE;

        public Declaration(string name, DeclarationType type, string declarationNamespace, List<IDependency> dependencies)
        {
            Name = name;
            Type = type;
            DeclarationNamespace = declarationNamespace;
            Dependencies = dependencies;
        }
        public override bool Equals(object? obj)
        {
            if (obj?.GetType() == typeof(Declaration))
            {
                return GetHashCode() == ((Declaration)obj).GetHashCode();
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        public override string ToString()
        {
            return $"{Name}-{Type}-{DeclarationNamespace}-{ContainingAssembly}";
        }
    }
}
