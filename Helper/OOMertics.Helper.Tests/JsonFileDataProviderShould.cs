using OOMertics.Helper.Implementations;
using OOMetrics.Abstractions;

namespace OOMertics.Helper.Tests
{
    public class JsonFileDataProviderShould : TestBase
    {
        [Fact]
        public async void ReadFromValidFile()
        {
            var provider = new JsonFileDataProvider("./TestData/testSolutionSerializedDependencies.json");
            var data = await provider.GetDeclarations();
            data.ToList().Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public async void ProperlySaveAndReadFromFile()
        {
            var provider = new SolutionDeclarationProvider($"{solutionLocation}{testSolutionDir}", testSolutionName);
            var declaraitons = await provider.GetDeclarations();

            JsonFileDataProvider.DumpIntoFile("./TestData/SolutionDeclarationProviderShould.ProvideDeclarations.json", declaraitons);
            var referenceData = JsonFileDataProvider.ReadFromFile("./TestData/SolutionDeclarationProviderShould.ProvideDeclarations.json");

            declaraitons.Should().BeEquivalentTo(referenceData, o => o.WithStrictOrdering());
        }
        [Theory]
        [InlineData("./TestData/NonExistingFile.json", "Can not read declarations from * due to: Could not find file*")]
        [InlineData("./TestData/emptyfile.json", "Can not read declarations from * due to: Can not serialize * to * containing *")]
        [InlineData("./TestData/wrongFormat.txt", "Can not read declarations from * due to: Unexpected character encountered while parsing value: *")]
        [InlineData("./TestData/someJsonObject.json", "Can not read declarations from * due to: Cannot deserialize the current JSON object*")]
        [InlineData("./TestData/someJsonObjectCollection.json", "Can not read declarations from * due to: Could not create an instance of type *")]
        public async void ThrowOnWrongFile(string path, string messagePattern)
        {
            Action act = () => JsonFileDataProvider.ReadFromFile(path);
            act.Should().Throw<Exception>()
                .WithMessage(messagePattern);
        }
    }
}
