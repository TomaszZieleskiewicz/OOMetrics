using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using OOMetrics.Abstractions.Enums;
using OOMetrics.Abstractions.Interfaces;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace OOMertics.Helper.Implementations
{
    public static class DeclarationFactory
    {
        // TO DO: Add static list of already created declarations for performance? Wait for performance issues. :)
        private static readonly DeclarationType[] AbstractTypes = new[] { DeclarationType.INTERFACE_TYPE, DeclarationType.ENUM_TYPE, DeclarationType.ABSTRACT_CLASS_TYPE };
        public static IDeclaration CreateFromSyntax(BaseTypeDeclarationSyntax declarationNode, SemanticModel semanticModel)
        {
            if (semanticModel == null)
            {
                throw new ArgumentNullException($"Semantic model is null for {declarationNode.Identifier}");
            }
            var symbol = semanticModel.GetSymbolInfo(declarationNode).Symbol;
            if (symbol == null)
            {
                throw new ArgumentNullException($"Symbol is null for {declarationNode.Identifier}");
            }
            var name = declarationNode.Identifier.ToString();
            var type = GetType(declarationNode);
            var containingPackage = symbol.ContainingAssembly.Name;

            return new Declaration(name, type, containingPackage, IsAbstract(type), FindDependencies(declarationNode, semanticModel));
        }
        private static ImmutableArray<IDeclaration> FindDependencies(BaseTypeDeclarationSyntax node, SemanticModel semanticModel)
        {
            var declarationNodes = node.DescendantNodes(n => true);

            var namedTypes = SearchFor<IdentifierNameSyntax>(declarationNodes, semanticModel);
            var expressionTypes = SearchFor<ExpressionSyntax>(declarationNodes, semanticModel);

            var uniqueDependencies = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
            uniqueDependencies.UnionWith(namedTypes);
            uniqueDependencies.UnionWith(expressionTypes);
            return uniqueDependencies.Select(d => (IDeclaration)CreateFromSymbol(d)).ToImmutableArray();
        }
        private static IEnumerable<INamedTypeSymbol> SearchFor<T>(IEnumerable<SyntaxNode> nodes, SemanticModel semanticModel) where T: SyntaxNode
        {
            return nodes
                .OfType<T>()
                .Select(es => semanticModel.GetTypeInfo(es).Type)
                .ToHashSet(SymbolEqualityComparer.Default)
                .OfType<INamedTypeSymbol>();
        }
        private static bool IsAbstract(DeclarationType type)
        {
            return AbstractTypes.Contains(type);
        }
        private static Declaration CreateFromSymbol(INamedTypeSymbol symbol)
        {
            var type = GetType(symbol);
            return new Declaration(symbol.Name, type, symbol.ContainingAssembly.Name, IsAbstract(type), new ImmutableArray<IDeclaration>());
        }
        private static DeclarationType GetType(BaseTypeDeclarationSyntax declarationNode)
        {
            return declarationNode switch
            {
                EnumDeclarationSyntax enumDeclaration => DeclarationType.ENUM_TYPE,
                ClassDeclarationSyntax classDeclaration => classDeclaration.Modifiers.Any(x => x.IsKind(SyntaxKind.AbstractKeyword)) ? DeclarationType.ABSTRACT_CLASS_TYPE : DeclarationType.CLASS_TYPE,
                InterfaceDeclarationSyntax interfaceDeclaration => DeclarationType.INTERFACE_TYPE,
                RecordDeclarationSyntax recordDeclaration => DeclarationType.RECORD_TYPE,
                StructDeclarationSyntax structDeclaration => DeclarationType.STRUCT_TYPE,
                _ => throw new ArgumentException($"Unknown declaration type: {declarationNode.GetType()} for {declarationNode.Identifier}")
            };
        }
        private static DeclarationType GetType(INamedTypeSymbol declarationSymbol)
        {
            return declarationSymbol.TypeKind switch
            {
                TypeKind.Class => declarationSymbol.IsAbstract ? DeclarationType.ABSTRACT_CLASS_TYPE : declarationSymbol.IsRecord ? DeclarationType.RECORD_TYPE : DeclarationType.CLASS_TYPE,
                TypeKind.Interface => DeclarationType.INTERFACE_TYPE,
                TypeKind.Enum => DeclarationType.ENUM_TYPE,
                TypeKind.Struct => DeclarationType.STRUCT_TYPE,
                _ => throw new ArgumentException($"Unknown type kind: {declarationSymbol.TypeKind} for {declarationSymbol.Name}")
            };
        }
    }
}