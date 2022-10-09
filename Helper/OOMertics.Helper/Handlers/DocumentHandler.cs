using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OOMertics.Helper.Handlers
{
    public class DocumentHandler
    {
        private readonly Document Document;
        public readonly SemanticModel SemanticModel;
        public readonly List<DeclarationHandler> Declarations;
        private DocumentHandler(SemanticModel semanticModel, Document document)
        {
            SemanticModel = semanticModel;
            Document = document;
            Declarations = createDeclarations();
        }
        public static async Task<DocumentHandler> CreateFromDocumentAsync(Document document)
        {
            if (!document.SupportsSyntaxTree)
            {
                throw new ArgumentException($"Document {document.Name} is not supporting syntax tree");
            }
            var semanticModel = await document.GetSemanticModelAsync();
            if (semanticModel is null)
            {
                throw new ArgumentException($"Document {document.Name} semantic model is null");
            }
            return new DocumentHandler(semanticModel, document);
        }
        private List<DeclarationHandler> createDeclarations()
        {
            var syntaxNodes = SemanticModel.SyntaxTree.GetRoot().DescendantNodes(n => true);
            return syntaxNodes.OfType<BaseTypeDeclarationSyntax>().Select(btds => new DeclarationHandler(btds, SemanticModel)).ToList();
        }
        public override string ToString()
        {
            return Document.Name;
        }
    }
}
