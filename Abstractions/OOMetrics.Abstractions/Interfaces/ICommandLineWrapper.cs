namespace OOMertics.Abstractions.Interfaces
{
    public interface ICommandLineWrapper
    {
        bool WritingEnabled { get; set; }
        void WriteLine(string text, int ident = 0, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black);
    }
}
