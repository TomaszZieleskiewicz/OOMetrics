using Microsoft.Extensions.Options;
using OOMertics.ConsoleViewer.Commands;
using OOMertics.Abstractions.Interfaces;

namespace OOMertics.ConsoleViewer
{
    public class Runner : IRunner
    {
        private AnalyzeSolution _analyzeSolution;
        private CommandLineParameters _options;
        public Runner(AnalyzeSolution analyze, IOptions<CommandLineParameters> options)
        {
            _analyzeSolution = analyze;
            _options = options.Value;
        }
        public void Run()
        {
            switch (_options.Command)
            {
                case "analyze":
                    _analyzeSolution.ExecuteAsync();
                    break;
                default:
                    throw new Exception($"Unrecognized command: {_options.Command}");
            }
        }
    }
}
