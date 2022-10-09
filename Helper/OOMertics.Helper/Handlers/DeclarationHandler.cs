using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OOMetrics.Abstractions.Enums;

namespace OOMertics.Helper.Handlers
{
    public class DeclarationHandler
    {
        private readonly SemanticModel SemanticModel;

        public readonly string Name;
        public readonly DeclarationType Type;
        public readonly string ContainingAssemblyName;
        public readonly HashSet<INamedTypeSymbol> Dependencies = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        public DeclarationHandler(BaseTypeDeclarationSyntax declarationNode, SemanticModel semanticModel)
        {
            if(semanticModel == null)
            {
                throw new ArgumentNullException($"Semantic model null for {declarationNode.Identifier}");
            }
            SemanticModel = semanticModel;
            ContainingAssemblyName = SemanticModel.GetSymbolInfo(declarationNode).Symbol?.ContainingAssembly.Name;
            Name = declarationNode.Identifier.ToString();

            switch (declarationNode)
            {
                case EnumDeclarationSyntax enumDeclaration:
                    Type = DeclarationType.ENUM_TYPE;
                    break;
                case ClassDeclarationSyntax classDeclaration:
                    var isAbstract = classDeclaration.Modifiers.Any(x => x.IsKind(SyntaxKind.AbstractKeyword));
                    Type = isAbstract ? DeclarationType.ABSTRACT_CLASS_TYPE : DeclarationType.CLASS_TYPE;
                    break;
                case InterfaceDeclarationSyntax interfaceDeclaration:
                    Type = DeclarationType.INTERFACE_TYPE;
                    break;
                case RecordDeclarationSyntax recordDeclaration:
                    Type = DeclarationType.RECORD_TYPE;
                    break;
                case StructDeclarationSyntax structDeclaration:
                    Type = DeclarationType.STRUCT_TYPE;
                    break;
                default:
                    throw new ArgumentException($"Unknown declaration type: {declarationNode.GetType()} for {Name}");
            }

            FindDependencies(declarationNode);
        }
        public DeclarationHandler(INamedTypeSymbol declarationSymbol, SemanticModel semanticModel)
        {
            SemanticModel = semanticModel;
            ContainingAssemblyName = declarationSymbol.ContainingAssembly.Name;
            Name = declarationSymbol.Name;
            switch (declarationSymbol.TypeKind)
            {
                case TypeKind.Class:
                    Type = declarationSymbol.IsAbstract? DeclarationType.ABSTRACT_CLASS_TYPE : declarationSymbol.IsRecord? DeclarationType.RECORD_TYPE: DeclarationType.CLASS_TYPE;
                    break;
                case TypeKind.Interface:
                    Type = DeclarationType.INTERFACE_TYPE;
                    break;
                case TypeKind.Enum:
                    Type = DeclarationType.ENUM_TYPE;
                    break;
                case TypeKind.Struct:
                    Type = DeclarationType.STRUCT_TYPE;
                    break;
                default :
                    throw new ArgumentException($"Unknown type kind: {declarationSymbol.TypeKind} for {Name}");
            }
        }

        private void FindDependencies(BaseTypeDeclarationSyntax node)
        {
            var declarationNodes = node.DescendantNodes(n => true);
            var namedTypes = declarationNodes
                .OfType<IdentifierNameSyntax>()
                .Select(ins => SemanticModel.GetSymbolInfo(ins).Symbol)
                .ToHashSet(SymbolEqualityComparer.Default)
                .OfType<INamedTypeSymbol>();

            var expressionTypes = declarationNodes
                .OfType<ExpressionSyntax>()
                .Select(es => SemanticModel.GetTypeInfo(es).Type)
                .ToHashSet(SymbolEqualityComparer.Default)
                .OfType<INamedTypeSymbol>();

            Dependencies.UnionWith(namedTypes);
            Dependencies.UnionWith(expressionTypes);
        }
        public override string ToString()
        {
            return $"{Name}({Type})";
        }
    }
}
