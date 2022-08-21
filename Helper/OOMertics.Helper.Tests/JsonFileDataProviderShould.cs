using OOMertics.Helper.Implementations;

namespace OOMertics.Helper.Tests
{
    public class JsonFileDataProviderShould : TestBase
    {
        [Fact]
        public async void ReadFromValidFile()
        {
            var provider = new JsonFileDataProvider("./TestData/testSolutionSerializedDependencies.json");
            var data = provider.GetDeclarations().ToList();
            data.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public async void ProperlySaveAndReadFromFile()
        {
            var provider = new SolutionDeclarationProvider($"{solutionLocation}{testSolutionDir}", testSolutionName);
            await provider.Load();
            var declaraitons = provider.GetDeclarations();

            JsonFileDataProvider.DumpIntoFile("./TestData/SolutionDeclarationProviderShould.ProvideDeclarations.json", declaraitons);
            var referenceData = JsonFileDataProvider.ReadFromFile("./TestData/SolutionDeclarationProviderShould.ProvideDeclarations.json");

            declaraitons.Should().BeEquivalentTo(referenceData, o => o.WithStrictOrdering());
        }
    }
}
