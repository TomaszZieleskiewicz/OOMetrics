using OOMertics.Helper.Implementations;
using OOMetrics.Abstractions.Enums;

namespace OOMertics.Helper.Tests
{
    public class DeclarationShould
    {
        [Fact]
        public void CreateNewInstance()
        {
            var declaration = new Declaration("TestDeclaration", DeclarationType.CLASS_TYPE, "TestNamespace", "TestProject");
            declaration.Name.Should().Be("TestDeclaration");
        }
    }
}
