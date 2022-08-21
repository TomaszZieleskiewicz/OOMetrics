using OOMetrics.Metrics.Interfaces;

namespace OOMertics.Helper.Implementations
{
    public class Declaration : IDeclaration
    {
        public string Name { get; }
        public DeclarationType Type { get; }
        public string DeclarationNamespace { get; }
        public string ContainingAssembly { get; }
        public ICollection<IDependency> Dependencies { get; private set; }
        public bool IsAbstract => Type == DeclarationType.INTERFACE_TYPE || Type == DeclarationType.ABSTRACT_CLASS_TYPE;
        public Declaration(string name, DeclarationType type, string declarationNamespace, string containingAssembly)
        {
            Name = name;
            Type = type;
            DeclarationNamespace = declarationNamespace;
            Dependencies = new List<IDependency>();
            ContainingAssembly = containingAssembly;
        }
        public void AddDependency(Dependency dependency)
        {
            Dependencies.Add(dependency);
        }
        public override string ToString()
        {
            return $"{Name}, {Type}, {DeclarationNamespace}";
        }
    }
}
