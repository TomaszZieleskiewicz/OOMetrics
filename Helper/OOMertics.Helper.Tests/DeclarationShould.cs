using OOMertics.Helper.Implementations;
using OOMetrics.Abstractions.Enums;
using OOMetrics.Abstractions.Interfaces;
using System.Collections.Immutable;

namespace OOMertics.Helper.Tests
{
    public class DeclarationShould
    {
        [Fact]
        public void CreateNewInstance()
        {
            var declaration = new Declaration("TestDeclaration", DeclarationType.CLASS_TYPE, "TestProject", false, new ImmutableArray<IDeclaration>());
            declaration.Name.Should().Be("TestDeclaration");
            declaration.ToString().Should().Be("TestDeclaration(CLASS_TYPE) from TestNamespace in TestProject");
        }
    }
}
