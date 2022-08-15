﻿using OOMertics.Helper.Handlers;
using OOMetrics.Metrics.Interfaces;

namespace OOMertics.Helper.Implementations
{
    public class SolutionDeclarationProvider : IDeclarationProvider
    {
        private string path;
        private string solutionName;
        public SolutionDeclarationProvider(string path, string solutionName)
        {
            this.path = path;
            this.solutionName = solutionName;
        }
        public async Task<List<IDeclaration>> GetDeclarations()
        {
            var solutionHandler = await SolutionHandler.OpenAsync(path, solutionName);
            var declarations = new List<IDeclaration>();
            foreach (var project in solutionHandler.Projects)
            {
                foreach (var document in project.Documents)
                {
                    foreach (var rawDeclaration in document.Declarations)
                    {
                        var declaration = new Declaration(rawDeclaration.Name, rawDeclaration.Type, rawDeclaration.Namespace);
                        foreach (var dependency in rawDeclaration.Dependencies)
                        {
                            declaration.AddDependency(new Dependency(dependency.Name, dependency.ContainingNamespace.Name));
                        }
                        declarations.Add(declaration);
                    }
                }
            }
            return declarations;
        }
    }
}