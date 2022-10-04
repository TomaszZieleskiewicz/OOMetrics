using Microsoft.Extensions.Options;
using OOMertics.Helper.Implementations;
using OOMetrics.Abstractions.Abstract;
using OOMetrics.Abstractions.Enums;
using OOMetrics.Abstractions.Interfaces;

namespace OOMertics.Helper.Tests
{
    public class SolutionDeclarationProviderShould
    {
        [Fact]
        public async void ProvideDeclarations()
        {
            var options = Options.Create(new SolutionDeclarationProviderOptions { Path = $"{TestPathBase.SolutionLocation}{TestPathBase.TestSolutionDir}", SolutionName = TestPathBase.TestSolutionName });
            var provider = new SolutionDeclarationProvider(options);
            var declaraitons = await provider.GetDeclarations();
            declaraitons.Count().Should().Be(18);
            var testClass = declaraitons.Where(declaration => declaration.Name == "ClassUsingTypesFromOtherProject").First();

            testClass.Dependencies.Count().Should().Be(5);
            testClass.Type.Should().Be(DeclarationType.CLASS_TYPE);
            testClass.DeclarationNamespace.Should().Be("OtherTestProject");
            testClass.ContainingPackage.Should().Be("OtherTestProject");

            var abstractClass = declaraitons.Where(declaration => declaration.Name == "AbstractClass").First();
            abstractClass.Dependencies.Count().Should().Be(1);
            abstractClass.Type.Should().Be(DeclarationType.ABSTRACT_CLASS_TYPE);
            abstractClass.DeclarationNamespace.Should().Be("AbstractStableProject");
            abstractClass.ContainingPackage.Should().Be("AbstractStableProject");
            abstractClass.ToString().Should().Be("Abstract AbstractClass(ABSTRACT_CLASS_TYPE) from AbstractStableProject in AbstractStableProject");
        }
    }
}
