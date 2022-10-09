using OOMertics.Helper.Implementations;
using OOMetrics.Abstractions.Abstract;
using OOMetrics.Abstractions.Enums;
using OOMetrics.Abstractions.Interfaces;

namespace OOMetrics.Metrics.Tests.TestImplementations
{
    public class TestDeclaration : ComparableByStringHash, IDeclaration
    {
        private static readonly DeclarationType[] AbstractTypes = new[] { DeclarationType.INTERFACE_TYPE, DeclarationType.ENUM_TYPE, DeclarationType.ABSTRACT_CLASS_TYPE };
        public string Name { get; }
        public DeclarationType Type { get; }
        public string DeclarationNamespace { get; }
        public string ContainingPackage { get; }
        public ICollection<IDeclaration> Dependencies { get; } = new List<IDeclaration>();
        public bool IsAbstract => AbstractTypes.Contains(Type);
        public TestDeclaration(string name, DeclarationType type, string declarationNamespace, string containingPackage)
        {
            Name = name;
            Type = type;
            DeclarationNamespace = declarationNamespace;
            ContainingPackage = containingPackage;
        }
        public void AddDependency(IDeclaration dependency)
        {
            Dependencies.Add(dependency);
        }
        public override string ToString()
        {
            return $"{Name}({Type}) from {DeclarationNamespace} in {ContainingPackage}";
        }
    }
}
