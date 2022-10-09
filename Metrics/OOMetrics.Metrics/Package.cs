using OOMetrics.Abstractions.Interfaces;
using System.Globalization;

namespace OOMetrics.Metrics
{
    public class Package
    {
        public string Name { get; }
        public int EfferenCoupling { get => OutgoingDependencies.Count(); }
        public int AfferenCoupling { get => IncomingDependencies.Count(); }
        public decimal Instability { get => SafeDivide(EfferenCoupling, EfferenCoupling + AfferenCoupling); }
        public decimal Abstractness { get => SafeDivide(GetAbstractDeclarations().Count(), Declarations.Count()); }
        public decimal DistanceFromMainSequence { get => Math.Abs(Instability + Abstractness - 1); }
        // TO DO: Add cohesion metric from https://www.researchgate.net/publication/31598248_A_Validation_of_Martin's_Metric
        public ICollection<IDeclaration> Declarations { get; } = new List<IDeclaration>();
        public ICollection<IDeclaration> OutgoingDependencies { get; private set; } = new List<IDeclaration>();
        public ICollection<IDeclaration> IncomingDependencies { get; private set; } = new List<IDeclaration>();
        public Package(string name)
        {
            Name = name;
        }
        public void AddDeclaration(IDeclaration declaration)
        {
            AddIfNew(Declarations, declaration);
        }
        public void AddIncomingDependency(IDeclaration incomingDependency)
        {
            AddIfNew(IncomingDependencies, incomingDependency);
        }
        public void AddOutgoingDependency(IDeclaration outgoingDependency)
        {
            AddIfNew(OutgoingDependencies, outgoingDependency);
        }
        public override string ToString()
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            return $"{Name} ({DistanceFromMainSequence.ToString("0.0##", culture)})";
        }
        public IEnumerable<IDeclaration> GetAbstractDeclarations()
        {
            return Declarations.Where(d => d.IsAbstract);
        }
        private static void AddIfNew<T>(ICollection<T> list, T dependency) where T : IComparableByStringHash
        {
            var isNew = !list.Any(d => d != null && d.CompareByStringHash(dependency));
            if (isNew)
            {
                list.Add(dependency);
            }
        }
        private static decimal SafeDivide(int a, int b)
        {
            if (b == 0)
            {
                return 0;
            }
            else
            {
                return decimal.Divide(a, b);
            }
        }
    }
}
