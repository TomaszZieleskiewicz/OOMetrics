using FluentAssertions;
using OOMertics.Helper.Implementations;
using System.Text.Json;

namespace OOMertics.Helper.Tests
{
    public class SolutionDeclarationProviderShould : TestBase
    {
        [Fact]
        public async void ProvideDeclarations()
        {
            var provider = new SolutionDeclarationProvider($"{solutionLocation}{testSolutionDir}", testSolutionName);
            var declaraitons = await provider.GetDeclarations();
            declaraitons.Count().Should().Be(16);
            var options = new JsonSerializerOptions { WriteIndented = false };
            var serialized = JsonSerializer.Serialize(declaraitons, options);
            var referenceJson = File.ReadAllText("./TestData/testSolutionSerializedDependencies.json");
            serialized.Should().Be(referenceJson);
        }
    }
}
