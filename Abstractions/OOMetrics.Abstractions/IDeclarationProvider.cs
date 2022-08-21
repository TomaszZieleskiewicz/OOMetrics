namespace OOMetrics.Abstractions
{
    public interface IDeclarationProvider
    {
        Task<ICollection<IDeclaration>> GetDeclarations();
    }
}