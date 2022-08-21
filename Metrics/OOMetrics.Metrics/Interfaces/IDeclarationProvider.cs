namespace OOMetrics.Metrics.Interfaces
{
    public interface IDeclarationProvider
    {
        Task<ICollection<IDeclaration>> GetDeclarations();
    }
}