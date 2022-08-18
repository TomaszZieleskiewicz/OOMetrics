using OOMertics.Helper.Implementations;

namespace OOMetrics.Metrics.Tests
{
    public class MetricsCalculatorShould
    {
        [Fact]
        public async void CreateWithValidData()
        {
            var provider = new JsonFileDataProvider("./TestData/testSolutionSerializedDependencies.json");
            var declarations = provider.GetDeclarations();
            var calculator = new MetricsCalculator(declarations.ToList());
        }
    }
}
