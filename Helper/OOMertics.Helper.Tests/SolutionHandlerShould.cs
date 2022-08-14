using FluentAssertions;
using System.Reflection;

namespace OOMertics.Helper.Tests
{
    public class SolutionHandlerShould
    {
        private static string solutionLocation = @"../../../../../";
        private static string testSolutionDir = @"TestData/TestSolution";
        private static string testSolutionName = "TestSolution";
        [Theory]
        [InlineData(@"", "OOMetrics")]
        [InlineData(@"TestData/TestSolution", "TestSolution")]
        public async void ProperlyLoadSolutions(string path, string solutionName)
        {
            var pathA = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var solutionHandler = await SolutionHandler.OpenAsync($"{solutionLocation}{path}", solutionName);
            solutionHandler.Projects.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public async void ProperlyLoadProjects()
        {
            var solutionHandler = await SolutionHandler.OpenAsync($"{solutionLocation}{testSolutionDir}", testSolutionName);
            var projects = solutionHandler.Projects;
            projects.Count.Should().Be(2);
            projects.Where(p => p.AssemblyName == "TestProject").Count().Should().Be(1);
            projects.Where(p => p.AssemblyName == "OtherTestProject").Count().Should().Be(1);

            var testProjest = projects.Where(p => p.AssemblyName == "TestProject").Single();
            testProjest.Documents.Count().Should().Be(9, $"because this list is not correct: {string.Join(", ",testProjest.Documents)}");
            testProjest.Documents.First().ToString().Should().Be("ClassWithInterface.cs");
        }
    }
}