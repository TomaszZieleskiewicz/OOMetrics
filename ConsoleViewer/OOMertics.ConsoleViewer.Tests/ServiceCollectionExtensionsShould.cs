using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OOMertics.ConsoleViewer.Extensions;

namespace OOMertics.ConsoleViewer.Tests
{
    public class ServiceCollectionExtensionsShould
    {
        [Fact]
        public void Properly_Parse_Correct_Args()
        {
            // Arrange
            var services = new ServiceCollection();
            var path = "E:\\Poligon\\github\\OOMetrics";
            var args = new string[] { "-p", path, "-s", "OOMetrics.sln" };
            // Act
            services.AddCommandLineOptions(args);
            // Assert
            var provider = services.BuildServiceProvider();
            var options = provider.GetService<IOptions<CommandLineParameters>>();
            options.Should().NotBeNull();
            options?.Value.Command.Should().Be("analyze");
            options?.Value.Path.Should().Be(path);
        }
        [Fact]
        public void Throw_On_Missing_Mandatory_Args()
        {
            // Arrange
            var services = new ServiceCollection();
            var args = new string[] { };
            services.AddCommandLineOptions(args);
            var provider = services.BuildServiceProvider();
            var options = provider.GetService<IOptions<CommandLineParameters>>();
            var act = () => options?.Value.Command;
            // Act and assert
            act.Should().Throw<Exception>().WithMessage("*Required option 'p, path' is missing*");
        }
        [Fact]
        public void Not_Throw_When_Help_Requested()
        {
            // Arrange
            var services = new ServiceCollection();
            var args = new string[] { "--help"};
            services.AddCommandLineOptions(args);
            var provider = services.BuildServiceProvider();
            var options = provider.GetService<IOptions<CommandLineParameters>>();
            var act = () => options?.Value.Command;
            // Act and assert
            act.Should().NotThrow<Exception>();
        }
    }
}
