using AutoMapper;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OOMetrics.Abstractions.Interfaces;

namespace OOMertics.ConsoleViewer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandLineOptions(this IServiceCollection services, string[] args)
        {
            services.AddOptions<CommandLineParameters>()
                .Configure(opt =>
                {
                    using var writer = new StringWriter();
                    var parser = new Parser(configuration =>
                    {
                        configuration.AutoHelp = true;
                        configuration.AutoVersion = false;
                        configuration.CaseSensitive = false;
                        configuration.IgnoreUnknownArguments = true;
                        configuration.HelpWriter = writer;
                    });
                    var result = parser.ParseArguments(() => opt, args);
                    result.WithNotParsed(errors => HandleErrors(errors, writer));
                }
            );
            void HandleErrors(IEnumerable<Error> errors, TextWriter writer)
            {
                if (errors.Any(e => e.Tag != ErrorType.HelpRequestedError && e.Tag != ErrorType.VersionRequestedError))
                {
                    string message = $"{writer} {string.Join(" ", errors.Select(e => e.Tag))}";
                    throw new Exception(message);
                }
            }
            return services;
        }
        public static IServiceCollection AddMappedOptions<T>(this IServiceCollection services) where T: class
        {
            services.AddOptions<T>().Configure<IOptions<CommandLineParameters>>((opt, options) =>
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<CommandLineParameters, T>());
                var mapper = new Mapper(config);
                mapper.Map(options.Value, opt);
            });
            return services;
        }
    }
}
