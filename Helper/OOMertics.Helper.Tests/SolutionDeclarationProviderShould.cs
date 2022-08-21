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
            declaraitons.Count().Should().Be(18);
            var testClass = declaraitons.Where(declaration => declaration.Name == "ClassUsingTypesFromOtherProject").First();

            testClass.Dependencies.Count().Should().Be(5);
            testClass.Type.Should().Be(DeclarationType.CLASS_TYPE);
            testClass.DeclarationNamespace.Should().Be("OtherTestProject");
            testClass.ContainingAssembly.Should().Be("OtherTestProject");

            var abstractClass = declaraitons.Where(declaration => declaration.Name == "AbstractClass").First();
            abstractClass.Dependencies.Count().Should().Be(1);
            abstractClass.Type.Should().Be(DeclarationType.ABSTRACT_CLASS_TYPE);
            abstractClass.DeclarationNamespace.Should().Be("AbstractStableProject");
            abstractClass.ContainingAssembly.Should().Be("AbstractStableProject");
        }
    }
}
