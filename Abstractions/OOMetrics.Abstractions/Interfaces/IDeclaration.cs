using OOMetrics.Abstractions.Enums;

namespace OOMetrics.Abstractions.Interfaces
{
    public interface IDeclaration : IComparableByStringHash
    {
        string Name { get; }
        DeclarationType Type { get; }
        string ContainingPackage { get; }
        bool IsAbstract { get; }
        ICollection<IDeclaration> Dependencies { get; }
    }
}
