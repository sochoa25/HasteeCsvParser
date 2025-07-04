using CsvParser.Application;
using CsvParser.Domain.Interfaces;
using CsvParser.Infrastructure.ApiClients;
using CsvParser.Infrastructure.Parsers;
using CsvParser.Infrastructure.Parsers.Customer;
using CsvParser.Infrastructure.Presentation;
using Microsoft.Extensions.DependencyInjection;

namespace CsvParser.ConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Validate args
            if (args.Length == 0)
            {
                Console.WriteLine("Provide a CSV file path.");
                throw new ArgumentNullException(nameof(args));
            }

            if (args.Length == 1)
            {
                Console.WriteLine("Provide a parse type.");
                throw new ArgumentNullException(nameof(args));
            }

            string csvPath = args[0];
            string parserType = args[1];

            Console.WriteLine($"Processing CSV Path: {csvPath} and type {parserType}");


            // App configuration
            ServiceProvider serviceProvider = ConfigApp();

            // Run program
            var router = serviceProvider.GetRequiredService<ICsvProcessorRouter>();


            if (!router.CanHandle(parserType))
            {
                Console.WriteLine($"No processor registered for parser type: {parserType}");
                return;
            }

            var processorResult = await router.ProcessAsync(parserType, csvPath);

            // Print results
            var presenter = serviceProvider.GetRequiredService<IResultPresenter>();

            await presenter.ShowResultsAsync(processorResult);
        }

        private static ServiceProvider ConfigApp()
        {
            // Configure Services Container
            var services = new ServiceCollection();

            // Register services
            services.AddSingleton<CustomerTypeAParser>();
            services.AddSingleton<CustomerTypeBParser>();

            services.AddSingleton<ICustomerApi, CustomerApi>();
            services.AddSingleton<IResultPresenter, ConsoleJsonResultPresenter>();

            services.AddTransient<ICsvProcessorRouter, CsvProcessorRouter>();

            return services.BuildServiceProvider();
        }
    }
}