namespace OOMetrics.Metrics.Interfaces
{
    public interface IDeclarationProvider
    {
        IEnumerable<IDeclaration> GetDeclarations();
    }
}