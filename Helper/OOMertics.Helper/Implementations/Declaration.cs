using OOMetrics.Abstractions;

namespace OOMertics.Helper.Implementations
{
    public class Declaration : IDeclaration
    {
        public string Name { get; }
        public DeclarationType Type { get; }
        public string DeclarationNamespace { get; }
        public string ContainingPackage { get; }
        public ICollection<IDependency> Dependencies { get; } = new List<IDependency>();
        public bool IsAbstract => Type == DeclarationType.INTERFACE_TYPE || Type == DeclarationType.ABSTRACT_CLASS_TYPE;
        public Declaration(string name, DeclarationType type, string declarationNamespace, string containingPackage)
        {
            Name = name;
            Type = type;
            DeclarationNamespace = declarationNamespace;
            ContainingPackage = containingPackage;
        }
        public void AddDependency(IDependency dependency)
        {
            Dependencies.Add(dependency);
        }
        public IDependency ToDependency()
        {
            return new Dependency(Name, DeclarationNamespace, ContainingPackage);
        }
        public override bool Equals(object? obj)
        {
            return (obj is null) ? base.Equals(obj) : GetHashCode() == obj.GetHashCode();
        }
        public override int GetHashCode()
        {
            var stringRepresentation = ToString();
            if(stringRepresentation == null)
            {
                return 0;
            }
            return stringRepresentation.GetHashCode();
        }
        public override string ToString()
        {
            return $"{Name}({Type}) from {DeclarationNamespace} in {ContainingPackage}";
        }
    }
}
