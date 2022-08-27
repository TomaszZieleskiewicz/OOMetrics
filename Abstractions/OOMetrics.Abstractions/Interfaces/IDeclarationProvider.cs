namespace OOMetrics.Abstractions.Interfaces
{
    public interface IDeclarationProvider
    {
        Task<ICollection<IDeclaration>> GetDeclarations();
    }
}