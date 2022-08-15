﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OOMertics.Helper.Handlers
{
    public class DeclarationHandler
    {
        private readonly BaseTypeDeclarationSyntax DeclarationNode;
        private readonly DocumentHandler ParentDocument;

        public readonly string Name;
        public readonly DeclarationType Type;
        public readonly string Namespace;
        public readonly HashSet<INamedTypeSymbol> Dependencies = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
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
            Namespace = GetNamespace(declarationNode);
            ParentDocument = parentDocument;
            FindDependencies();
        }
        private void FindDependencies()
        {
            var declarationNodes = DeclarationNode.DescendantNodes(n => true);
            var namedTypes = declarationNodes
                .OfType<IdentifierNameSyntax>()
                .Select(ins => ParentDocument.SemanticModel.GetSymbolInfo(ins).Symbol)
                .ToHashSet(SymbolEqualityComparer.Default)
                .OfType<INamedTypeSymbol>();

            var expressionTypes = declarationNodes
                .OfType<ExpressionSyntax>()
                .Select(es => ParentDocument.SemanticModel.GetTypeInfo(es).Type)
                .ToHashSet(SymbolEqualityComparer.Default)
                .OfType<INamedTypeSymbol>();

            Dependencies.UnionWith(namedTypes);
            Dependencies.UnionWith(expressionTypes);
        }
        // From https://github.com/dotnet/runtime/blob/25c675ff78e0446fe596cea25c7e3969b0936a33/src/libraries/Microsoft.Extensions.Logging.Abstractions/gen/LoggerMessageGenerator.Parser.cs#L438
        private string GetNamespace(BaseTypeDeclarationSyntax syntax)
        {
            string nameSpace = string.Empty;
            SyntaxNode? potentialNamespaceParent = syntax.Parent;

            while (potentialNamespaceParent != null &&
                    potentialNamespaceParent is not NamespaceDeclarationSyntax
                    && potentialNamespaceParent is not FileScopedNamespaceDeclarationSyntax)
            {
                potentialNamespaceParent = potentialNamespaceParent.Parent;
            }

            if (potentialNamespaceParent is BaseNamespaceDeclarationSyntax namespaceParent)
            {
                nameSpace = namespaceParent.Name.ToString();
                while (true)
                {
                    if (namespaceParent.Parent is not NamespaceDeclarationSyntax parent)
                    {
                        break;
                    }
                    nameSpace = $"{namespaceParent.Name}.{nameSpace}";
                    namespaceParent = parent;
                }
            }

            return nameSpace;
        }
        public override string ToString()
        {
            return $"{Name}({Type})";
        }
    }
}
