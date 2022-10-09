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
                        var declaration = new Declaration(rawDeclaration.Name, rawDeclaration.Type, rawDeclaration.ContainingAssemblyName);
                        /*
                        foreach (var rawDependency in rawDeclaration.Dependencies.Select(d => new DeclarationHandler(d, document) ))
                        {
                            declaration.AddDependency(new Declaration(rawDependency.Name, rawDependency.Type, rawDependency.ContainingAssemblyName));
                        }
                        */
                        declarations.Add(declaration);
                    }
                }
            }
            return declarations;
        }
    }
}