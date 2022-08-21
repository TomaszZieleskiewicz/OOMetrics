using OOMertics.Helper.Implementations;

namespace OOMetrics.Metrics.Tests
{
    public class MetricsCalculatorShould
    {
        [Fact]
        public async void AnalyzeData()
        {
            var provider = new JsonFileDataProvider("./TestData/testSolutionSerializedDependencies.json");
            var declarations = provider.GetDeclarations();
            var calculator = new MetricsCalculator(declarations.ToList());
            calculator.AnalyzeData();
            var packages = calculator.Packages;
            packages.Count().Should().Be(3);

            var testProject = packages.Where(p => p.Name == "TestProject").First();
            testProject.Declarations.Count().Should().Be(12);
            testProject.IncomingDependencies.Count().Should().Be(3);
            testProject.OutgoingDependencies.Should().BeEmpty();

            var otherTestProject = packages.Where(p => p.Name == "OtherTestProject").First();
            otherTestProject.Declarations.Count().Should().Be(4);
            otherTestProject.IncomingDependencies.Should().BeEmpty(); 
            otherTestProject.OutgoingDependencies.Count().Should().Be(6);
        }
    }
}
