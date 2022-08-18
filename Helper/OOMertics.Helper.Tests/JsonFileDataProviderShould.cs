using OOMertics.Helper.Implementations;
using OOMetrics.Metrics.Interfaces;

namespace OOMertics.Helper.Tests
{
    public class JsonFileDataProviderShould
    {
        [Fact]
        public async void ReadFromValidFile()
        {
            var provider = new JsonFileDataProvider("./TestData/testSolutionSerializedDependencies.json");
            var data = provider.GetDeclarations().ToList();
            data.Count.Should().BeGreaterThan(0);
        }
    }
}
