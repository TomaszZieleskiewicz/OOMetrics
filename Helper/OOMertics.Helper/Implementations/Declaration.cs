using OOMetrics.Abstractions.Enums;
using OOMetrics.Abstractions.Interfaces;
using System.Collections.Immutable;

namespace OOMertics.Helper.Implementations
{
    public record Declaration(string Name, DeclarationType Type, string ContainingPackage, bool IsAbstract, ImmutableArray<IDeclaration> Dependencies) : IDeclaration
    {
        /*
        public override string ToString()
        {
            return $"{(IsAbstract ? "Abstract " : "")}{Name}({Type}) in {ContainingPackage}";
        }
        */
        public bool Equals(IDeclaration? other)
        {
            return base.Equals(other);
        }
    }
}