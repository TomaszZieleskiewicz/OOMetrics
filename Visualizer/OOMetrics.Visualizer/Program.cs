using OOMertics.Helper.Implementations;
using OOMetrics.Metrics;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var provider = new SolutionDeclarationProvider($"tr", "OOMetrics");
            /*
             * var declarations = await provider.GetDeclarations();
            var calculator = new MetricsCalculator(declarations.ToList());
            calculator.AnalyzeData();
            var packages = calculator.Packages;
            var totalDistance = packages.Sum(p => p.DistanceFromMainSequence);
            */
        }
    }
}