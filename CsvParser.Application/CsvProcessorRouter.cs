using CsvParser.Domain.DTOs;
using CsvParser.Domain.Interfaces;
using CsvParser.Domain.Models;
using CsvParser.Infrastructure.Parsers.Customer;
using Microsoft.Extensions.DependencyInjection;


namespace CsvParser.Application
{
    public class CsvProcessorRouter: ICsvProcessorRouter
    {
        private readonly Dictionary<string, Func<string, Task<ProcessorResult>>> _routes = new();

        public CsvProcessorRouter(IServiceProvider provider)
        {
            Register<CustomerDto>("customerTypeA", provider.GetRequiredService<CustomerTypeAParser>);
            Register<CustomerDto>("customerTypeB", provider.GetRequiredService<CustomerTypeBParser>);
        }

        private void Register<T>(
            string parserType,
            Func<ICsvParser<T>> getParser
        )
        {
            _routes[parserType.ToLower()] = async filePath =>
            {
                var parser = getParser();
                var processor = new CsvProcessor<T>(parser);
                var (successes, failures) = await processor.ProcessAsync(filePath);

                return new ProcessorResult
                {
                    DtoType = typeof(T),
                    Successes = successes.ToList(),
                    Failures = failures.ToList()
                };
            };
        }

        public Task<ProcessorResult> ProcessAsync(string parserType, string filePath)
        {
            if (!_routes.TryGetValue(parserType.ToLower(), out var handler))
                throw new InvalidOperationException($"Unknown parserType '{parserType}'");

            return handler(filePath);
        }

        public bool CanHandle(string parserType) => _routes.ContainsKey(parserType.ToLower());
    }
}
