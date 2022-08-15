using OOMetrics.Metrics.Interfaces;

namespace OOMertics.Helper.Implementations
{
    public class Declaration: IDeclaration
    {
        public string Name { get; }
        public DeclarationType Type { get; }
        public string Namespace { get; }
        public List<IDependency> Dependencies { get; }
        public Declaration(string name, DeclarationType type, string declarationNamespace)
        {
            Name = name;
            Type = type;
            Namespace = declarationNamespace;
            Dependencies = new List<IDependency>();
        }
        public void AddDependency(Dependency dependency)
        {
            Dependencies.Add(dependency);
        }
        public override string ToString()
        {
            return $"{Name}, {Type}, {Namespace}";
        }
    }
}
