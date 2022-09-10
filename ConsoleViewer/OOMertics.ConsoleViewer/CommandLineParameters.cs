using CommandLine;

namespace OOMertics.ConsoleViewer
{
    public class CommandLineParameters
    {
        [Option('c', "command", Required = false, HelpText = "Command that you can execute", Default = "analyze")]
        public string Command { get; set; } = "analyze";
        [Option('p', "path", Required = true, HelpText = "Path to the solution")]
        public string Path { get; set; } = string.Empty;
    }
}
