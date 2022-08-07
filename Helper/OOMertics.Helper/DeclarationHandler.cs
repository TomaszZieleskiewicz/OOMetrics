using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OOMertics.Helper
{
    public class DeclarationHandler
    {
        private readonly BaseTypeDeclarationSyntax DeclarationNode;
        private readonly DocumentHandler ParentDocument;

        public readonly string Name;
        public readonly DeclarationType Type;
        public readonly HashSet<INamedTypeSymbol> Dependencies = new HashSet<INamedTypeSymbol>();
        public DeclarationHandler(BaseTypeDeclarationSyntax declarationNode, DocumentHandler parentDocument)
        {
            DeclarationNode = declarationNode;
            ParentDocument = parentDocument;
            switch (declarationNode)
            {
                case EnumDeclarationSyntax enumDeclaration:
                    Name = enumDeclaration.Identifier.ToString();
                    Type = DeclarationType.ENUM_TYPE;
                    break;
                case ClassDeclarationSyntax classDeclaration:
                    Name = classDeclaration.Identifier.ToString();
                    Type = DeclarationType.CLASS_TYPE;
                    break;
                case InterfaceDeclarationSyntax interfaceDeclaration:
                    Name = interfaceDeclaration.Identifier.ToString();
                    Type = DeclarationType.INTERFACE_TYPE;
                    break;
                case RecordDeclarationSyntax recordDeclaration:
                    Name = recordDeclaration.Identifier.ToString();
                    Type = DeclarationType.RECORD_TYPE;
                    break;
                case StructDeclarationSyntax structDeclaration:
                    Name = structDeclaration.Identifier.ToString();
                    Type = DeclarationType.STRUCT_TYPE;
                    break;
                default:
                    throw new ArgumentException($"Unknown declaration type: {declarationNode.GetType()}");
            }

            this.ParentDocument = parentDocument;
            FindDependencies();
        }
        private void FindDependencies()
        {
            var declarationNodes = DeclarationNode.DescendantNodes(n => true);
            var namedTypes = declarationNodes
                .OfType<IdentifierNameSyntax>()
                .Select(ins => ParentDocument.SemanticModel.GetSymbolInfo(ins).Symbol)
                // .Where(nt => nt.ContainingNamespace.ToString().StartsWith("ArchitectureGuards"))
                .ToHashSet(SymbolEqualityComparer.Default)
                .OfType<INamedTypeSymbol>();

            var expressionTypes = declarationNodes
                .OfType<ExpressionSyntax>()
                .Select(es => ParentDocument.SemanticModel.GetTypeInfo(es).Type)
                //.Where(nt => nt.ContainingNamespace.ToString().StartsWith("ArchitectureGuards"))
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
