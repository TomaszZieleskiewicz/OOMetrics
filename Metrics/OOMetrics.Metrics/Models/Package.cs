using OOMetrics.Metrics.Interfaces;

namespace OOMetrics.Metrics.Models
{
    public class Package
    {
        public string Name { get; }
        public int EfferenCoupling { get => OutgoingDependencies.Count(); }
        public int AfferenCoupling { get => IncomingDependencies.Count(); }
        public decimal Instability { get => SafeDivide(EfferenCoupling, EfferenCoupling + AfferenCoupling); }
        public decimal Abstractness { get => SafeDivide(Declarations.Where(d=>d.IsAbstract).Count(), Declarations.Count()); }
        public decimal DistanceFromMainSequence { get => Math.Abs(Instability + Abstractness - 1); }
        public ICollection<IDeclaration> Declarations { get; } = new List<IDeclaration>();
        public ICollection<IDependency> OutgoingDependencies { get; private set; } = new List<IDependency>();
        public ICollection<IDependency> IncomingDependencies { get; private set; } = new List<IDependency>();
        public Package(string name)
        {
            Name = name;
        }
        public void AddDeclaration(IDeclaration declaration)
        {
            AddIfNew(Declarations, declaration);
        }
        public void AddIncomingDependency(IDependency incomingDependency)
        {
            AddIfNew(IncomingDependencies, incomingDependency);
        }
        public void AddOutgoingDependency(IDependency outgoingDependency)
        {
            AddIfNew(OutgoingDependencies, outgoingDependency);
        }
        public override string ToString()
        {
            return Name;
        }
        private void AddIfNew<T>(ICollection<T> list, T dependency)
        {
            var isNew = list.Where(d => d.Equals(dependency)).Count() == 0;
            if (isNew)
            {
                list.Add(dependency);
            }
        }
        private decimal SafeDivide(int a, int b)
        {
            if(b == 0)
            {
                return 0;
            } 
            else
            {
                return Decimal.Divide(a, b);
            }
        }
    }
}
