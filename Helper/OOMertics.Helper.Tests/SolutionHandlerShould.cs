using FluentAssertions;

namespace OOMertics.Helper.Tests
{
    public class SolutionHandlerShould
    {
        private static string testSolutionDir = @"E:\Poligon\github\OOMetrics\TestData\TestSolution";
        private static string testSolutionName = "TestSolution";
        [Theory]
        [InlineData(@"E:\Poligon\github\OOMetrics\", "OOMetrics")]
        [InlineData(@"E:\Poligon\github\OOMetrics\TestData\TestSolution", "TestSolution")]
        public async void ProperlyLoadSolutions(string path, string solutionName)
        {
            var solutionHandler = await SolutionHandler.OpenAsync($"{path}", solutionName);
            solutionHandler.Projects.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public async void ProperlyLoadProjects()
        {
            var solutionHandler = await SolutionHandler.OpenAsync($"{testSolutionDir}", testSolutionName);
            var projects = solutionHandler.Projects;
            projects.Count.Should().Be(2);
            projects.Where(p => p.AssemblyName == "TestProject").Count().Should().Be(1);
            projects.Where(p => p.AssemblyName == "OtherTestProject").Count().Should().Be(1);

            var testProjest = projects.Where(p => p.AssemblyName == "TestProject").Single();
            testProjest.Documents.Count().Should().Be(9);
            testProjest.Documents.First().ToString().Should().Be("ClassWithInterface.cs");
        }
    }
}