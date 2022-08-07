using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OOMertics.Helper
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

            return new DocumentHandler(await document.GetSemanticModelAsync(), document);
        }
        private List<DeclarationHandler> createDeclarations()
        {
            var syntaxNodes = SemanticModel.SyntaxTree.GetRoot().DescendantNodes(n => true);
            return syntaxNodes.OfType<BaseTypeDeclarationSyntax>().Select(btds => new DeclarationHandler(btds, this)).ToList();
        }
        public override string ToString()
        {
            return Document.Name;
        }
    }
}
