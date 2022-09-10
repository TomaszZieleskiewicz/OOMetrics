using Microsoft.Extensions.DependencyInjection;
using OOMertics.Abstractions.Interfaces;

namespace OOMertics.ConsoleViewer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = Startup.ConfigureServices(args);
            var runner = services.BuildServiceProvider().GetService<IRunner>();
            if (runner == null)
            {
                throw new NullReferenceException("Can not find runner instance in service collection");
            }
            runner.Run();
        }
    }
}