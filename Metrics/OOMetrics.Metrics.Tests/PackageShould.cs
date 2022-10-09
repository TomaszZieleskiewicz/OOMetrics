using OOMetrics.Abstractions.Enums;
using OOMetrics.Metrics.Tests.TestImplementations;

namespace OOMetrics.Metrics.Tests
{
    public class PackageShould
    {
        [Fact]
        public void ProperlyCalculateOnEmptyPackage()
        {
            var packageName = "packageA";
            var package = new Package(packageName);

            package.Declarations.Count().Should().Be(0);
            package.IncomingDependencies.Should().BeEmpty();
            package.OutgoingDependencies.Should().BeEmpty();
            package.EfferenCoupling.Should().Be(0);
            package.AfferenCoupling.Should().Be(0);
            package.Instability.Should().Be(0);
            package.Abstractness.Should().Be(0);
            package.DistanceFromMainSequence.Should().Be(1);
            package.ToString().Should().Be($"{packageName} (1.0)");
        }
        [Fact]
        public void ProperlyCalculateOnSimplePackage()
        {
            var packageName = "packageA";
            var IA = new TestDeclaration("IA", DeclarationType.INTERFACE_TYPE, "namespaceA", packageName);
            var A = new TestDeclaration("A", DeclarationType.CLASS_TYPE, "namespaceA", packageName);
            var package = new Package(packageName);
            package.AddDeclaration(A);
            package.AddDeclaration(IA);

            package.Declarations.Count().Should().Be(2);
            package.IncomingDependencies.Should().BeEmpty();
            package.OutgoingDependencies.Should().BeEmpty();
            package.EfferenCoupling.Should().Be(0);
            package.AfferenCoupling.Should().Be(0);
            package.Instability.Should().Be(0);
            package.Abstractness.Should().Be(0.5M);
            package.DistanceFromMainSequence.Should().Be(0.5M);
            package.ToString().Should().Be($"{packageName} (0.5)");
        }
        [Fact]
        public void ProperlyCalculateOnMoreComplexPackage()
        {
            var packageName = "packageA";
            var IA = new TestDeclaration("IA", DeclarationType.INTERFACE_TYPE, "namespaceA", packageName);
            var A = new TestDeclaration("A", DeclarationType.CLASS_TYPE, "namespaceA", packageName);
            var InD1 = new TestDeclaration("InD1", DeclarationType.CLASS_TYPE, "InDN", "InDPackage");
            var InD2 = new TestDeclaration("InD2", DeclarationType.CLASS_TYPE, "InDN", "InDPackage");
            var InD3 = new TestDeclaration("InD3", DeclarationType.CLASS_TYPE, "InD3N", "InD3Package");
            var OutD3 = new TestDeclaration("OutD3", DeclarationType.CLASS_TYPE, "OutD3N", "OutD3Package");
            var package = new Package(packageName);
            package.AddDeclaration(A);
            package.AddDeclaration(IA);
            package.AddIncomingDependency(InD1);
            package.AddIncomingDependency(InD2);
            package.AddIncomingDependency(InD3);
            package.AddOutgoingDependency(OutD3);
            package.Declarations.Count().Should().Be(2);
            package.IncomingDependencies.Count().Should().Be(3);
            package.OutgoingDependencies.Count().Should().Be(1);
            package.EfferenCoupling.Should().Be(1);
            package.AfferenCoupling.Should().Be(3);
            package.Instability.Should().Be(0.25M);
            package.Abstractness.Should().Be(0.5M);
            package.DistanceFromMainSequence.Should().Be(0.25M);
            package.ToString().Should().Be($"{packageName} (0.25)");
        }
        [Fact]
        public void NotAddTheSameObjectTwice()
        {
            var packageName = "packageA";
            var IA = new TestDeclaration("IA", DeclarationType.INTERFACE_TYPE, "namespaceA", packageName);
            var IAp = new TestDeclaration("IA", DeclarationType.INTERFACE_TYPE, "namespaceA", packageName);
            var A = new TestDeclaration("A", DeclarationType.CLASS_TYPE, "namespaceA", packageName);
            var Ap = new TestDeclaration("A", DeclarationType.CLASS_TYPE, "namespaceA", packageName);
            var InD1 = new TestDeclaration("InD1", DeclarationType.CLASS_TYPE, "InDN", "InDPackage");
            var InD1p = new TestDeclaration("InD1", DeclarationType.CLASS_TYPE, "InDN", "InDPackage");
            var InD2 = new TestDeclaration("InD2", DeclarationType.INTERFACE_TYPE, "InDN", "InDPackage");
            var InD2p = new TestDeclaration("InD2", DeclarationType.INTERFACE_TYPE, "InDN", "InDPackage");
            var InD3 = new TestDeclaration("InD3", DeclarationType.CLASS_TYPE, "InD3N", "InD3Package");
            var InD3p = new TestDeclaration("InD3", DeclarationType.CLASS_TYPE, "InD3N", "InD3Package");
            var OutD3 = new TestDeclaration("OutD3", DeclarationType.ABSTRACT_CLASS_TYPE, "OutD3N", "OutD3Package");
            var OutD3p = new TestDeclaration("OutD3", DeclarationType.ABSTRACT_CLASS_TYPE, "OutD3N", "OutD3Package");
            var package = new Package(packageName);
            package.AddDeclaration(A);
            package.AddDeclaration(Ap);
            package.AddDeclaration(IA);
            package.AddDeclaration(IAp);
            package.AddIncomingDependency(InD1);
            package.AddIncomingDependency(InD2);
            package.AddIncomingDependency(InD3);
            package.AddOutgoingDependency(OutD3);
            package.AddIncomingDependency(InD1p);
            package.AddIncomingDependency(InD2p);
            package.AddIncomingDependency(InD3p);
            package.AddOutgoingDependency(OutD3p);
            package.Declarations.Count().Should().Be(2);
            package.IncomingDependencies.Count().Should().Be(3);
            package.OutgoingDependencies.Count().Should().Be(1);
            package.EfferenCoupling.Should().Be(1);
            package.AfferenCoupling.Should().Be(3);
            package.Instability.Should().Be(0.25M);
            package.Abstractness.Should().Be(0.5M);
            package.DistanceFromMainSequence.Should().Be(0.25M);
            package.ToString().Should().Be($"{packageName} (0.25)");
        }
    }
}
