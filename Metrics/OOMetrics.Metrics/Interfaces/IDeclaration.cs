using OOMertics.Helper;

namespace OOMetrics.Metrics.Interfaces
{
    public interface IDeclaration
    {
        string Name { get; }
        DeclarationType Type { get; }
        string Namespace { get; }
        List<IDependency> Dependencies { get; }
    }
}
