using System.Text.Json;
using CsvParser.Domain.Interfaces;
using CsvParser.Domain.Models;

namespace CsvParser.Infrastructure.Presentation
{
    public class ConsoleJsonResultPresenter : IResultPresenter
    {
        public Task ShowResultsAsync(ProcessorResult result)
        {
            Console.WriteLine("- Successes:");
            foreach (var success in result.Successes)
            {
                Console.WriteLine(JsonSerializer.Serialize(success, new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
            }

            Console.WriteLine();
            Console.WriteLine("- Failures:");
            foreach (var (row, errors) in result.Failures)
            {
                Console.WriteLine($"Row: [{string.Join(", ", row)}]");
                Console.WriteLine("Errors:");
                foreach (var error in errors)
                    Console.WriteLine($" - {error}");
            }

            return Task.CompletedTask;
        }
    }

}
