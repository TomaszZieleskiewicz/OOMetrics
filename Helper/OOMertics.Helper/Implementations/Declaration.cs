using OOMetrics.Abstractions.Enums;
using OOMetrics.Abstractions.Interfaces;
using System.Collections.Immutable;

namespace OOMertics.Helper.Implementations
{
    public sealed record Declaration(string Name, DeclarationType Type, string ContainingPackage, ImmutableArray<IDeclaration> Dependencies) : IDeclaration
    {
        private static readonly DeclarationType[] AbstractTypes = new[] { DeclarationType.INTERFACE_TYPE, DeclarationType.ENUM_TYPE, DeclarationType.ABSTRACT_CLASS_TYPE };
        public bool IsAbstract => AbstractTypes.Contains(Type);
        public bool Equals(Declaration? other)
        {
            return this.GetHashCode() == other?.GetHashCode();
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        public override string ToString()
        {
            return $"{(IsAbstract ? "Abstract " : "")}{Name}({Type}) in {ContainingPackage}";
        }
    }
}