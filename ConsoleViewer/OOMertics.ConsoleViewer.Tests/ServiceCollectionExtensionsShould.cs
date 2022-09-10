using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OOMertics.Abstractions.Interfaces;
using OOMertics.ConsoleViewer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var args = new string[] { "-p", path };
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
            act.Should().Throw<Exception>().WithMessage(" MissingRequiredOptionError");
        }
    }
}
