using OOMetrics.Abstractions.Abstract;
using OOMetrics.Abstractions.Enums;
using OOMetrics.Abstractions.Interfaces;

namespace OOMertics.Helper.Implementations
{
    public class Declaration : ComparableByStringHash, IDeclaration
    {
        private static readonly DeclarationType[] AbstractTypes = new[] { DeclarationType.INTERFACE_TYPE, DeclarationType.ENUM_TYPE, DeclarationType.ABSTRACT_CLASS_TYPE };
        public string Name { get; }
        public DeclarationType Type { get; }
        public string ContainingPackage { get; }
        public ICollection<IDeclaration> Dependencies { get; } = new List<IDeclaration>();
        public bool IsAbstract => AbstractTypes.Contains(Type);
        public Declaration(string name, DeclarationType type, string containingPackage)
        {
            Name = name;
            Type = type;
            ContainingPackage = containingPackage;
        }
        public void AddDependency(IDeclaration dependency)
        {
            Dependencies.Add(dependency);
        }
        public override string ToString()
        {
            return $"{(IsAbstract?"Abstract ":"")}{Name}({Type}) in {ContainingPackage}";
        }
    }
}