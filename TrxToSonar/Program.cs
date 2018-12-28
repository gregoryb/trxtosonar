using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TrxToSonar.Model.Sonar;
using TrxToSonar.Model.Trx;

namespace TrxToSonar
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            var app = new CommandLineApplication
            {
                Name = "Trx To Sonar",
                Description = ""
            };

            app.HelpOption("-?|-h|--help");
 
            var solutionDirectoryOption = app.Option("-d","Solution Directory to parse.",CommandOptionType.SingleValue);
            var outputOption = app.Option("-o", "Output filename.", CommandOptionType.SingleValue);
            var absolutePathOption = app.Option("-a|--absolute", "Use Absolute Path", CommandOptionType.NoValue);
 
            app.OnExecute(() => {
                if (solutionDirectoryOption.HasValue() && outputOption.HasValue())
                {
                    var converter = serviceProvider.GetService<IConverter>();
                    var sonarDocument = converter.Parse(solutionDirectoryOption.Value(), absolutePathOption.HasValue());
                    converter.Save(sonarDocument, outputOption.Value());
                }
                else {
                    app.ShowHint();
                }
 
                return 0;
            });
 
            app.Execute(args);
        }
        
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(new LoggerFactory().AddConsole());
            serviceCollection.AddLogging();
            serviceCollection.AddSingleton<IConverter, Converter>();
        }
    }
}