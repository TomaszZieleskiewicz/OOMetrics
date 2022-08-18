using OOMertics.Helper;

namespace OOMetrics.Metrics.Interfaces
{
    public interface IDeclaration
    {
        string Name { get; }
        DeclarationType Type { get; }
        string DeclarationNamespace { get; }
        IEnumerable<IDependency> Dependencies { get; }
    }
}
