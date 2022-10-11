using OOMetrics.Abstractions.Enums;
using OOMetrics.Abstractions.Interfaces;
using System.Collections.Immutable;

namespace OOMetrics.Metrics.Tests.TestImplementations
{
    public record TestDeclaration(string Name, DeclarationType Type, string ContainingPackage, ImmutableArray<IDeclaration> Dependencies) : IDeclaration
    {
        private static readonly DeclarationType[] AbstractTypes = new[] { DeclarationType.INTERFACE_TYPE, DeclarationType.ENUM_TYPE, DeclarationType.ABSTRACT_CLASS_TYPE };
        public bool IsAbstract => AbstractTypes.Contains(Type);
        public bool Equals(IDeclaration? other)
        {
            return base.Equals(other);
        }
    }
}
