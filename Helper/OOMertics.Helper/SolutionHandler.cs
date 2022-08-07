using System.Linq;
using System.Collections.Generic;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.CSharp;

namespace OOMertics.Helper
{
    public class SolutionHandler
    {
        private readonly Workspace Workspace;

        public readonly List<ProjectHandler> Projects;
        private SolutionHandler(Workspace workspace, List<ProjectHandler> projects)
        {
            this.Workspace = workspace;
            this.Projects = projects;
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
            return new SolutionHandler(workspace, await GetSolutionProjectsAsync(workspace));
        }

        private static async Task<List<ProjectHandler>> GetSolutionProjectsAsync(Workspace workspace)
        {
            return workspace.CurrentSolution.Projects.Select(async (project) => await ProjectHandler.CreateFromProjectAsync(project)).Select(t => t.Result).ToList();
        }
    }
}