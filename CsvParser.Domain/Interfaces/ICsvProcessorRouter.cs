using CsvParser.Domain.Models;

namespace CsvParser.Domain.Interfaces
{
    public interface ICsvProcessorRouter
    {
        bool CanHandle(string parserType);

        /// <summary>
        /// Processes a CSV file using the parser associated with the specified parser type.
        /// </summary>
        /// <param name="parserType">The type of parser to use for processing the CSV file.</param>
        /// <param name="filePath">The path to the CSV file to process.</param>
        /// <returns>
        /// A <see cref="Task{ProcessorResult}"/> representing the asynchronous operation, containing the processing result.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the specified parser type is not registered.
        /// </exception>
        Task<ProcessorResult> ProcessAsync(string parserType, string filePath);
    }
}