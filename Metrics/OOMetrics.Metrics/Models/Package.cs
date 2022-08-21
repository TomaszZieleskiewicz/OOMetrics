using OOMetrics.Metrics.Interfaces;

namespace OOMetrics.Metrics.Models
{
    public class Package
    {
        public string Name { get; }
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
        private void AddIfNew<T>(ICollection<T> list, T dependency)
        {
            var isNew = list.Where(d => d.Equals(dependency)).Count() == 0;
            if (isNew)
            {
                list.Add(dependency);
            }
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
