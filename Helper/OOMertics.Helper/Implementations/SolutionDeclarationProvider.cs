using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Extensions.Options;
using OOMetrics.Abstractions.Interfaces;

namespace OOMertics.Helper.Implementations
{
    public class SolutionDeclarationProvider : IDeclarationProvider
    {
        private SolutionDeclarationProviderOptions _options;

        public SolutionDeclarationProvider(IOptions<SolutionDeclarationProviderOptions> options)
        {
            _options = options.Value;
        }
        public async Task<ICollection<IDeclaration>> GetDeclarations()
        {
            var solutionFilePath = Directory.GetFiles($"{_options.Path}", $"{_options.SolutionName}.sln", SearchOption.AllDirectories).Single();
            if (!MSBuildLocator.IsRegistered)
            {
                MSBuildLocator.RegisterDefaults();
            }

            var workspace = MSBuildWorkspace.Create();

            await workspace.OpenSolutionAsync(solutionFilePath);
            var projects = workspace.CurrentSolution.Projects;
            var declarations = new List<IDeclaration>();
            foreach (var project in projects)
            {
                var compilation = await project.GetCompilationAsync();
                var documents = projects.SelectMany(p => p.Documents).ToArray();
                foreach (var document in documents)
                {
                    var semanticModel = await document.GetSemanticModelAsync();
                    if (semanticModel == null)
                    {
                        throw new Exception($"Can not find semantic model for {document.Name}");
                    }
                    var syntaxNodes = semanticModel.SyntaxTree.GetRoot().DescendantNodes(n => true).ToArray();
                    declarations = declarations.Concat(syntaxNodes.OfType<BaseTypeDeclarationSyntax>().Select(d => DeclarationFactory.CreateFromSyntax(d, semanticModel))).ToList();
                }
            }
            return declarations;
        }
    }
}