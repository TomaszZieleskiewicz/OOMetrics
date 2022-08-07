using FluentAssertions;

namespace OOMertics.Helper.Tests
{
    public class Tests
    {
        [Theory]
        [InlineData(@"E:\Poligon\github\ArchitectureGuards\ArchitectureGuards", "ArchitectureGuards")]
        [InlineData(@"E:\Poligon\github\OOMetrics", "OOMetrics")]
        public async void ProperlyLoadSolutions(string path, string solution)
        {
            var solutionHandler = await SolutionHandler.OpenAsync(path, solution);
            var projects = solutionHandler.getProjects();
            projects.Count().Should().BeGreaterThan(0);
            var notLoadedProjects = projects
                .Where(p => !p.HasDocuments)
                .Select(p => p.Name)
                .ToList();
            notLoadedProjects.Count().Should().Be(0, $"The following projects have been loaded incorrectly: {string.Join(Environment.NewLine, notLoadedProjects)} {Environment.NewLine} See workspace.Diagnostics for details.");
        }
        [Theory]
        [InlineData(@"E:\Poligon\github\ArchitectureGuards\ArchitectureGuards", "ArchitectureGuards")]
        [InlineData(@"E:\Poligon\github\OOMetrics", "OOMetrics")]
        public async void GetDependencies(string path, string solution)
        {
            var solutionHandler = await SolutionHandler.OpenAsync(path, solution);
            var dependencies = await solutionHandler.getDependenciesAsync();
        }
    }
}