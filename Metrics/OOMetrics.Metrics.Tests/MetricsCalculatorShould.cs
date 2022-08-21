using OOMertics.Helper.Implementations;
using OOMertics.Helper.Tests;

namespace OOMetrics.Metrics.Tests
{
    public class MetricsCalculatorShould : TestBase
    {
        [Fact]
        public async void AnalyzeData()
        {
            var provider = new JsonFileDataProvider("./TestData/testSolutionSerializedDependencies.json");
            var declarations = await provider.GetDeclarations();
            var calculator = new MetricsCalculator(declarations.ToList());
            calculator.AnalyzeData();
            var packages = calculator.Packages;
            packages.Count().Should().Be(3);

            var testProject = packages.Where(p => p.Name == "TestProject").First();
            testProject.Declarations.Count().Should().Be(12);
            testProject.IncomingDependencies.Count().Should().Be(3);
            testProject.OutgoingDependencies.Should().BeEmpty();
            testProject.EfferenCoupling.Should().Be(0);
            testProject.AfferenCoupling.Should().Be(3);
            testProject.Instability.Should().Be(0);
            testProject.Abstractness.Should().BeApproximately(0.166M, 3);
            testProject.DistanceFromMainSequence.Should().BeApproximately(0.833M, 3);

            var otherTestProject = packages.Where(p => p.Name == "OtherTestProject").First();
            otherTestProject.Declarations.Count().Should().Be(4);
            otherTestProject.IncomingDependencies.Should().BeEmpty();
            otherTestProject.OutgoingDependencies.Count().Should().Be(4);
            otherTestProject.EfferenCoupling.Should().Be(4);
            otherTestProject.AfferenCoupling.Should().Be(0);
            otherTestProject.Instability.Should().Be(1);
            otherTestProject.Abstractness.Should().BeApproximately(0, 3);
            otherTestProject.DistanceFromMainSequence.Should().BeApproximately(0, 3);

            var abstractStableProject = packages.Where(p => p.Name == "AbstractStableProject").First();
            abstractStableProject.Declarations.Count().Should().Be(2);
            abstractStableProject.IncomingDependencies.Count().Should().Be(1);
            abstractStableProject.OutgoingDependencies.Should().BeEmpty();
            abstractStableProject.EfferenCoupling.Should().Be(0);
            abstractStableProject.AfferenCoupling.Should().Be(1);
            abstractStableProject.Instability.Should().Be(0);
            abstractStableProject.Abstractness.Should().BeApproximately(1, 3);
            abstractStableProject.DistanceFromMainSequence.Should().BeApproximately(0, 3);
        }
        [Fact]
        public async void AnalyzeThisSolution()
        {
            var provider = new SolutionDeclarationProvider($"{solutionLocation}", "OOMetrics");
            var declarations = await provider.GetDeclarations();
            var calculator = new MetricsCalculator(declarations.ToList());
            calculator.AnalyzeData();
            var packages = calculator.Packages;
        }
    }
}
