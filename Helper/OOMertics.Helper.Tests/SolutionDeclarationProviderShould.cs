using OOMertics.Helper.Implementations;
using OOMetrics.Metrics.Interfaces;
using Newtonsoft.Json;
using OOMetrics.Metrics.Models;
using System.IO;

namespace OOMertics.Helper.Tests
{
    public class SolutionDeclarationProviderShould : TestBase
    {
        [Fact]
        public async void ProvideDeclarations()
        {
            var provider = new SolutionDeclarationProvider($"{solutionLocation}{testSolutionDir}", testSolutionName);
            await provider.Load();
            var declaraitons = provider.GetDeclarations();
            declaraitons.Count().Should().Be(16);

            JsonFileDataProvider.DumpIntoFile("./TestData/testSolutionSerializedDependencies.json", declaraitons);
            var referenceData = JsonFileDataProvider.ReadFromFile("./TestData/testSolutionSerializedDependencies.json");
            declaraitons.Should().BeEquivalentTo(referenceData);
        }
    }
}
