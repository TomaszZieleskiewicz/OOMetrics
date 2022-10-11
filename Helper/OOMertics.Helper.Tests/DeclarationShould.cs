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
            var declaration = new Declaration("TestDeclaration", DeclarationType.CLASS_TYPE, "TestProject", new ImmutableArray<IDeclaration>());
            declaration.Name.Should().Be("TestDeclaration");
            declaration.ToString().Should().Be("TestDeclaration(CLASS_TYPE) in TestProject");
        }
        [Fact]
        public void CompareEqualDeclarationsWithoutDependenciesProperly()
        {
            var declaration1 = new Declaration("TestDeclaration", DeclarationType.CLASS_TYPE, "TestProject", new ImmutableArray<IDeclaration>());
            var declaration2 = new Declaration("TestDeclaration", DeclarationType.CLASS_TYPE, "TestProject", new ImmutableArray<IDeclaration>());
            declaration1.Equals(declaration2).Should().BeTrue();
        }
        [Fact]
        public void CompareEqualDeclarationsWithDependenciesProperly()
        {
            var dependency1 = new Declaration("TestDependency", DeclarationType.ENUM_TYPE, "TestProjectA", new ImmutableArray<IDeclaration>());
            var dependency2 = new Declaration("TestDependency", DeclarationType.ENUM_TYPE, "TestProjectA", new ImmutableArray<IDeclaration>());
            var depArray1 = new List<IDeclaration>() { dependency1 };
            var depArray2 = new List<IDeclaration>() { dependency2 };
            var declaration1 = new Declaration("TestDeclaration", DeclarationType.CLASS_TYPE, "TestProject", depArray1.ToImmutableArray());
            var declaration2 = new Declaration("TestDeclaration", DeclarationType.CLASS_TYPE, "TestProject", depArray2.ToImmutableArray());
            declaration1.Equals(declaration2).Should().BeTrue();
        }
    }
}
