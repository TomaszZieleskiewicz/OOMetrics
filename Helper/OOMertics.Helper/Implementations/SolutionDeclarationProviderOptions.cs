using OOMetrics.Abstractions.Interfaces;

namespace OOMertics.Helper.Implementations
{
    public class SolutionDeclarationProviderOptions: ISolutionDeclarationProviderOptions
    {
        public string Path { get; init; } = string.Empty;
        public string SolutionName { get; init; } = string.Empty;
    }
}
