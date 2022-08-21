using OOMertics.Helper.Handlers;
using OOMetrics.Metrics.Interfaces;

namespace OOMertics.Helper.Implementations
{
    public class SolutionDeclarationProvider : IDeclarationProvider
    {
        private readonly string path;
        private readonly string solutionName;
        private SolutionHandler solutionHandler;
        public SolutionDeclarationProvider(string path, string solutionName)
        {
            this.path = path;
            this.solutionName = solutionName;
        }
        public async Task Load()
        {
            solutionHandler = await SolutionHandler.OpenAsync(path, solutionName);
        }
        public ICollection<IDeclaration> GetDeclarations()
        {
            if (solutionHandler == null)
            {
                throw new Exception("Solution handler not loaded. Call Load first.");
            }
            var declarations = new List<IDeclaration>();
            foreach (var project in solutionHandler.Projects)
            {
                foreach (var document in project.Documents)
                {
                    foreach (var rawDeclaration in document.Declarations)
                    {
                        var declaration = new Declaration(rawDeclaration.Name, rawDeclaration.Type, rawDeclaration.Namespace, project.AssemblyName);
                        foreach (var dependency in rawDeclaration.Dependencies)
                        {
                            declaration.AddDependency(new Dependency(dependency.Name, dependency.ContainingNamespace.Name, dependency.ContainingAssembly.Name));
                        }
                        declarations.Add(declaration);
                    }
                }
            }
            return declarations;
        }
    }
}