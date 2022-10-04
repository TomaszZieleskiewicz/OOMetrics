using OOMertics.Abstractions.Interfaces;

namespace OOMertics.ConsoleViewer
{
    public class CommandLineWrapper : ICommandLineWrapper
    {
        public void WriteLine(string text, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
