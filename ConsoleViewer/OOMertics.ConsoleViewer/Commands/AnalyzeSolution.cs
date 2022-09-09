using Microsoft.Extensions.Options;
using OOMertics.Abstractions.Interfaces;

namespace OOMertics.ConsoleViewer.Commands
{
    public class AnalyzeSolution : ICommand
    {
        private ICommandLineWrapper _console;
        private CommandLineParameters _options;
        public AnalyzeSolution(ICommandLineWrapper console, IOptions<CommandLineParameters> options)
        {
            _console = console;
            _options = options.Value;
        }
        public void Execute()
        {
            _console.WriteLine($"Searching for .sln files in {_options.Path}");
        }
    }
}
