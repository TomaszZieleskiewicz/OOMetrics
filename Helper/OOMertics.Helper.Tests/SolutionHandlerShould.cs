using OOMertics.Helper.Handlers;
using OOMetrics.Abstractions;

namespace OOMertics.Helper.Tests
{
    public class SolutionHandlerShould : TestBase
    {
        private async Task<SolutionHandler> LoadTestSolution()
        {
            return await SolutionHandler.OpenAsync($"{solutionLocation}{testSolutionDir}", testSolutionName);
        }
        [Theory]
        [InlineData(@"", "OOMetrics")]
        [InlineData(@"TestData/TestSolution", "TestSolution")]
        public async void ProperlyLoadSolutions(string path, string solutionName)
        {
            var solutionHandler = await SolutionHandler.OpenAsync($"{solutionLocation}{path}", solutionName);
            solutionHandler.Projects.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public async void ProperlyLoadProjects()
        {
            var solutionHandler = await LoadTestSolution();
            var projects = solutionHandler.Projects;
            projects.Count.Should().Be(3);
            projects.Where(p => p.AssemblyName == "TestProject").Count().Should().Be(1);
            projects.Where(p => p.AssemblyName == "OtherTestProject").Count().Should().Be(1);

            var testProjest = projects.Where(p => p.AssemblyName == "TestProject").Single();
            testProjest.Documents.Where(p => p.ToString() == "ClassWithInterface.cs").Count().Should().Be(1);

            var testClass = testProjest.Documents.Where(p => p.ToString() == "SimpleClass.cs").First();
            testClass.Declarations.Count().Should().Be(1);

            var manuDeclarations = testProjest.Documents.Where(p => p.ToString() == "ManyDeclarationsInSingleFile.cs").First();
            manuDeclarations.Declarations.Count().Should().Be(4);
        }
        [Fact]
        public async void ProperlyFindDependencies()
        {
            var solutionHandler = await LoadTestSolution();
            var testProject = solutionHandler.Projects.Where(p => p.AssemblyName == "TestProject").First();
            var otherTestProject = solutionHandler.Projects.Where(p => p.AssemblyName == "OtherTestProject").First();
            var abstractStableProject = solutionHandler.Projects.Where(p => p.AssemblyName == "AbstractStableProject").First();

            var simpleClassDocument = testProject.Documents.Where(p => p.ToString() == "SimpleClass.cs").First();
            var simpleClassDeclaration = simpleClassDocument.Declarations.First();
            simpleClassDeclaration.Name.Should().Be("SimpleClass");
            simpleClassDeclaration.Namespace.Should().Be("TestProject"); ;
            simpleClassDeclaration.Type.Should().Be(DeclarationType.CLASS_TYPE);
            simpleClassDeclaration.Dependencies.Where(d => d.ContainingNamespace.Name == "TestProject").Count().Should().Be(0);

            var classWithInterfaceDocument = testProject.Documents.Where(p => p.ToString() == "ClassWithInterface.cs").First();
            var classWithInterfaceDeclaration = classWithInterfaceDocument.Declarations.First();
            classWithInterfaceDeclaration.Name.Should().Be("ClassWithInterface");
            classWithInterfaceDeclaration.Namespace.Should().Be("TestProject"); ;
            classWithInterfaceDeclaration.Type.Should().Be(DeclarationType.CLASS_TYPE);
            classWithInterfaceDeclaration.Dependencies.Where(d => d.ContainingNamespace.Name == "TestProject").Count().Should().Be(1);

            var abstractClass = abstractStableProject.Documents.Where(p => p.ToString() == "AbstractClass.cs").First();
            var abstractClassDeclaration = abstractClass.Declarations.First();
            abstractClassDeclaration.Name.Should().Be("AbstractClass");
            abstractClassDeclaration.Namespace.Should().Be("AbstractStableProject"); ;
            abstractClassDeclaration.Type.Should().Be(DeclarationType.ABSTRACT_CLASS_TYPE);
        }
    }
}