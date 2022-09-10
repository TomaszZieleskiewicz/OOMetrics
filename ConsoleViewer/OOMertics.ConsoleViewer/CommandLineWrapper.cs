using OOMertics.Abstractions.Interfaces;

namespace OOMertics.ConsoleViewer
{
    public class CommandLineWrapper : ICommandLineWrapper
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
