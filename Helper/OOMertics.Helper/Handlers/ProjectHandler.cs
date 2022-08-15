using Microsoft.CodeAnalysis;

namespace OOMertics.Helper.Handlers
{
    public class ProjectHandler
    {
        private readonly Project Project;

        public readonly string AssemblyName;
        public readonly List<DocumentHandler> Documents;

        private ProjectHandler(Project project, List<DocumentHandler> documents)
        {
            Project = project;
            AssemblyName = project.AssemblyName;
            Documents = documents;
        }
        public static async Task<ProjectHandler> CreateFromProjectAsync(Project project)
        {
            var documents = new List<DocumentHandler>();
            foreach (var document in project.Documents.Where(d => d.SupportsSemanticModel))
            {
                documents.Add(await DocumentHandler.CreateFromDocumentAsync(document));
            }
            return new ProjectHandler(project, documents);
        }
        public override string ToString()
        {
            return AssemblyName;
        }
    }
}
