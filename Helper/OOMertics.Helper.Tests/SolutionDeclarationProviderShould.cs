using FluentAssertions;
using OOMertics.Helper.Implementations;

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
        }
    }
}
