using OOMetrics.Abstractions.Enums;
using System.Collections.Immutable;

namespace OOMetrics.Abstractions.Interfaces
{
    public interface IDeclaration: IEquatable<IDeclaration>
    {
        string Name { get; init; }
        DeclarationType Type { get; init; }
        string ContainingPackage { get; init; }
        bool IsAbstract { get; init; }
        ImmutableArray<IDeclaration> Dependencies { get; init; }
    }
}
