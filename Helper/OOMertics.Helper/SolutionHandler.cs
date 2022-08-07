using System.Linq;
using System.Collections.Generic;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace OOMertics.Helper
{
    public class SolutionHandler
    {
        private readonly Workspace workspace;
        private SolutionHandler(Workspace workspace)
        {
            this.workspace = workspace;
        }
        public async static Task<SolutionHandler> OpenAsync(string path, string solutionName)
        {
            var solutionFilePath = Directory.GetFiles(path, $"{solutionName}.sln", SearchOption.AllDirectories).Single();
            if (!MSBuildLocator.IsRegistered)
            {
                MSBuildLocator.RegisterDefaults();
            }            
            var workspace = MSBuildWorkspace.Create();
            await workspace.OpenSolutionAsync(solutionFilePath);
            return new SolutionHandler(workspace);
        }
        public IEnumerable<Project> getProjects()
        {
            return workspace.CurrentSolution.Projects;
        }
        public async Task<Dictionary<string, List<string>>>  getProjectDependencies(string projectAssemblyName)
        {
            var project = getProjects().Single(project => project.AssemblyName == projectAssemblyName);
            var dependencies = new Dictionary<string, List<string>>();

            foreach (var document in project.Documents)
            {
                var semanticModel = await document.GetSemanticModelAsync();
                KeyValuePair<string, List<string>>? keyValue = null;

                foreach (var item in semanticModel.SyntaxTree.GetRoot().DescendantNodes())
                {
                    switch (item)
                    {
                        case ClassDeclarationSyntax classDeclaration:
                        case InterfaceDeclarationSyntax interfaceDeclaration:
                            if (!keyValue.HasValue)
                            {
                                keyValue = new KeyValuePair<string, List<string>>(semanticModel.GetDeclaredSymbol(item).Name, new List<string>());
                            }
                            break;
                        case SimpleBaseTypeSyntax simpleBaseTypeSyntax:
                            keyValue?.Value.Add(simpleBaseTypeSyntax.Type.ToString());
                            break;
                        case ParameterSyntax parameterSyntax:
                            keyValue?.Value.Add(parameterSyntax.Type?.ToString());
                            break;
                    }
                }

                if (keyValue.HasValue)
                {
                    dependencies.Add(keyValue.Value.Key, keyValue.Value.Value);
                }
            }
            return dependencies;
        }
        public async Task<Dictionary<string, Dictionary<string, List<string>>>> getDependenciesAsync()
        {
            var dependencies = new Dictionary<string, Dictionary<string, List<string>>>();
            //key - project assembly name, value - list of dependent class names with dependencies
            foreach(var project in getProjects())
            {
                dependencies.Add(project.AssemblyName, await getProjectDependencies(project.AssemblyName));
            }

            return dependencies;
        }
    }
}