using OOMertics.Helper.Implementations;
using OOMetrics.Abstractions;

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
            var options = new MetricsCalculatorOptions()
            {
                IgnoredDependencyNamespaces = new[] { "System" },
                IgnoredIncomingDependencyNamespaces = new[] { "OOMetrics.Metrics.Tests" }
            };
            var calculator = new MetricsCalculator(declarations.ToList(), options );
            calculator.AnalyzeData();
            var packages = calculator.Packages;
            var totalDistance = packages.Sum(p => p.DistanceFromMainSequence);
            // 2.7107142857142857142857142857M
            // 2.5107142857142857142857142857M
            // 1.5583333333333333333333333333M
            // 1.5444444444444444444444444444M
            // 0.8666666666666666666666666666M
            // 0.9083333333333333333333333333M
            // 0.3190476190476190476190476190M
            // 0.2857142857142857142857142857M
            // 0.1666666666666666666666666667M
        }
    }
}
