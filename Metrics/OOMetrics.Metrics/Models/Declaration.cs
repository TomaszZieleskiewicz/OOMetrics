using OOMertics.Helper;
using OOMetrics.Metrics.Interfaces;

namespace OOMetrics.Metrics.Models
{
    public class Declaration : IDeclaration
    {
        public string Name { get; }
        public DeclarationType Type { get; }
        public string DeclarationNamespace { get; }
        public IEnumerable<IDependency> Dependencies { get; }
        public Declaration(string name, DeclarationType type, string declarationNamespace, List<IDependency> dependencies)
        {
            Name = name;
            Type = type;
            DeclarationNamespace = declarationNamespace;
            Dependencies = dependencies;
        }
    }
}
