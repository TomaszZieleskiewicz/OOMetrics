using OOMertics.Abstractions.Interfaces;

namespace OOMertics.ConsoleViewer
{
    public class CommandLineWrapper : ICommandLineWrapper
    {
        public bool WritingEnabled { get; set; } = true;
        public void WriteLine(string text, int ident = 0, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            if (!WritingEnabled)
            {
                return;
            }
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine($"{new string('\t', ident)}{text}");
            Console.ResetColor();
        }
    }
}
