using Microsoft.Extensions.Options;
using OOMertics.Helper.Handlers;
using OOMetrics.Abstractions.Interfaces;

namespace OOMertics.Helper.Implementations
{
    public class SolutionDeclarationProvider : IDeclarationProvider
    {
        private SolutionHandler? solutionHandler;
        private SolutionDeclarationProviderOptions _options;

        public SolutionDeclarationProvider(IOptions<SolutionDeclarationProviderOptions> options)
        {
            _options = options.Value;
        }
        public async Task<ICollection<IDeclaration>> GetDeclarations()
        {
            solutionHandler ??= await SolutionHandler.OpenAsync(_options.Path, _options.SolutionName);

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