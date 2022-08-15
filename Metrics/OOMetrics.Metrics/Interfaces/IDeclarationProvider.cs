namespace OOMetrics.Metrics.Interfaces
{
    public interface IDeclarationProvider
    {
        Task<List<IDeclaration>> GetDeclarations();
    }
}
