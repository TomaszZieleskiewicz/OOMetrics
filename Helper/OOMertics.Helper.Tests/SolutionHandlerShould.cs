using OOMertics.Helper.Handlers;
using OOMetrics.Abstractions.Abstract;
using OOMetrics.Abstractions.Enums;

namespace OOMertics.Helper.Tests
{
    public class SolutionHandlerShould
    {
        private async Task<SolutionHandler> LoadTestSolution()
        {
            return await SolutionHandler.OpenAsync($"{TestPathBase.SolutionLocation}{TestPathBase.TestSolutionDir}", TestPathBase.TestSolutionName);
        }
        [Theory]
        [InlineData(@"", "OOMetrics")]
        [InlineData(@"TestData/TestSolution", "TestSolution")]
        public async void ProperlyLoadSolutions(string path, string solutionName)
        {
            var solutionHandler = await SolutionHandler.OpenAsync($"{TestPathBase.SolutionLocation}{path}", solutionName);
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
            testProjest.ToString().Should().Be("TestProject");
            var testClass = testProjest.Documents.Where(p => p.ToString() == "SimpleClass.cs").First();
            testClass.Declarations.Count().Should().Be(1);
            var declaration = testClass.Declarations.Where(d => d.Name == "SimpleClass").First();
            declaration.ToString().Should().Be("SimpleClass(CLASS_TYPE)");
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
            simpleClassDeclaration.Type.Should().Be(DeclarationType.CLASS_TYPE);
            // simpleClassDeclaration.Dependencies.Where(d => d.Identifier.ToString() == "TestProject").Count().Should().Be(0);

            var classWithInterfaceDocument = testProject.Documents.Where(p => p.ToString() == "ClassWithInterface.cs").First();
            var classWithInterfaceDeclaration = classWithInterfaceDocument.Declarations.First();
            classWithInterfaceDeclaration.Name.Should().Be("ClassWithInterface");
            classWithInterfaceDeclaration.Type.Should().Be(DeclarationType.CLASS_TYPE);
            // classWithInterfaceDeclaration.Dependencies.Where(d => d.Identifier.ToString() == "TestProject").Count().Should().Be(1);

            var abstractClass = abstractStableProject.Documents.Where(p => p.ToString() == "AbstractClass.cs").First();
            var abstractClassDeclaration = abstractClass.Declarations.First();
            abstractClassDeclaration.Name.Should().Be("AbstractClass");
            abstractClassDeclaration.Type.Should().Be(DeclarationType.ABSTRACT_CLASS_TYPE);
        }
    }
}