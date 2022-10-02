using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OOMertics.Abstractions.Interfaces;
using OOMertics.Helper.Implementations;
using System.IO;

namespace OOMertics.ConsoleViewer.Tests
{
    public class TestCommandLineWrapper : ICommandLineWrapper
    {
        public List<string> WrittenText = new List<string>();
        public void WriteLine(string text)
        {
            WrittenText.Add(text);
        }
    }
    public class RunnerShould
    {
        [Fact]
        public void Run_With_Proper_Params()
        {
            // Arrange
            var goodParams = new List<string> { "-p", "E:\\Poligon\\github\\OOMetrics", "-s", "OOMetrics" };
            (var runner, var commandLineWrapper) = ConfigureRunner(goodParams.ToArray());
            // Act
            runner.Run();
            // Assert
            commandLineWrapper.WrittenText.First().Should().Be($"Searching for .sln files in {goodParams[1]}");
        }
        [Fact]
        public void Throw_On_Unrecognized_Command()
        {
            // Arrange
            var invalidCommand = "NotValidCommand";
            var wrongParams = new List<string> { "-p", "E:\\Poligon\\github\\OOMetrics", "-c", invalidCommand, "-s", "OOMetrics"};
            (var runner, var commandLineWrapper) = ConfigureRunner(wrongParams.ToArray());
            Action act = () => runner.Run();
            // Act and Assert
            act.Should().Throw<Exception>().WithMessage($"Unrecognized command: {invalidCommand}");
        }
        private (IRunner, TestCommandLineWrapper) ConfigureRunner(string[] args)
        {
            var services = Startup.ConfigureServices(args);
            var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(ICommandLineWrapper));
            if (serviceDescriptor == null)
            {
                throw new NullReferenceException("ICommandLineWrapper not found");
            }
            services.Remove(serviceDescriptor);
            services.AddSingleton<ICommandLineWrapper, TestCommandLineWrapper>();

            var provider = services.BuildServiceProvider();
            var runner = provider.GetService<IRunner>();
            if (runner == null)
            {
                throw new NullReferenceException("Can not find runner instance in service collection");
            }
            var commandLineWrapper = provider.GetService<ICommandLineWrapper>();
            if (commandLineWrapper == null)
            {
                throw new NullReferenceException("Can not find ICommandLineWrapper instance in service collection");
            }
            
            return (runner, (TestCommandLineWrapper)commandLineWrapper);
        }
    }
}
