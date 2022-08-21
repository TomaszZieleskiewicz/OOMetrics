using OOMertics.Helper;

namespace OOMetrics.Metrics.Interfaces
{
    public interface IDeclaration
    {
        string Name { get; }
        DeclarationType Type { get; }
        string DeclarationNamespace { get; }
        string ContainingAssembly { get; }
        ICollection<IDependency> Dependencies { get; }
        public bool IsAbstract { get; }
    }
}
