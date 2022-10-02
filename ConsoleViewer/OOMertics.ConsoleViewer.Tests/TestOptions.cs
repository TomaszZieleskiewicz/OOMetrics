using OOMetrics.Abstractions.Interfaces;

namespace OOMertics.ConsoleViewer.Tests
{
    public class TestOptions : ISolutionDeclarationProviderOptions
    {
        public string Path { get; init; } = string.Empty;
        public string SolutionName { get; init; } = string.Empty;
    }
}
