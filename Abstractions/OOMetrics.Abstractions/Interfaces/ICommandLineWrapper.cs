namespace OOMertics.Abstractions.Interfaces
{
    public interface ICommandLineWrapper
    {
        void WriteLine(string text, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black);
    }
}
