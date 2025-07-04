using CsvParser.Domain.DTOs;

namespace CsvParser.Domain.Interfaces
{
    public interface ICsvProcessor<T>
    {

        /// <summary>
        /// Processes a CSV file at the specified file path by reading and parsing each row using the provided parser.
        /// Returns a tuple containing a list of successfully parsed objects (as <see cref="CommonDto"/>) and a list of failed rows with their associated errors.
        /// </summary>
        /// <param name="filePath">The path to the CSV file to process.</param>
        /// <returns>
        /// A tuple with two elements:
        /// - Successes: List of successfully parsed objects as <see cref="CommonDto"/>.
        /// - Failures: List of tuples, each containing the original row and a list of error messages.
        /// </returns>
        Task<(
            List<CommonDto> Successes,
            List<(string[] Row, List<string> Errors)> Failures
        )> ProcessAsync(string filePath);
    }
}
