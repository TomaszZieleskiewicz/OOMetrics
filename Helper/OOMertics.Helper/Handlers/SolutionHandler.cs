using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using OOMetrics.Metrics.Interfaces;

namespace OOMertics.Helper.Handlers
{
    public class SolutionHandler
    {
        private readonly Workspace Workspace;
        private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public readonly List<ProjectHandler> Projects;
        
        private SolutionHandler(Workspace workspace, List<ProjectHandler> projects)
        {
            Workspace = workspace;
            Projects = projects;
        }
        public async static Task<SolutionHandler> OpenAsync(string path, string solutionName)
        {
            await semaphore.WaitAsync();
            try
            {
                var solutionFilePath = Directory.GetFiles(path, $"{solutionName}.sln", SearchOption.AllDirectories).Single();
                if (!MSBuildLocator.IsRegistered)
                {
                    MSBuildLocator.RegisterDefaults();
                }
                var workspace = MSBuildWorkspace.Create();
                await workspace.OpenSolutionAsync(solutionFilePath);
                return new SolutionHandler(workspace, GetSolutionProjectsAsync(workspace));
            }
            finally
            {
                semaphore.Release();
            }
        }
        private static List<ProjectHandler> GetSolutionProjectsAsync(Workspace workspace)
        {
            return workspace.CurrentSolution.Projects.Select(async (project) => await ProjectHandler.CreateFromProjectAsync(project)).Select(t => t.Result).ToList();
        }
    }
}