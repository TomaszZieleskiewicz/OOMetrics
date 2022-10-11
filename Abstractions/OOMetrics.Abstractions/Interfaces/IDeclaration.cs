using OOMetrics.Abstractions.Enums;
using System.Collections.Immutable;

namespace OOMetrics.Abstractions.Interfaces
{
    public interface IDeclaration
    {
        string Name { get; init; }
        DeclarationType Type { get; init; }
        string ContainingPackage { get; init; }
        bool IsAbstract { get; }
        ImmutableArray<IDeclaration> Dependencies { get; init; }
    }
}
