using CsvParser.Domain.Common;
using CsvParser.Domain.DTOs;
using CsvParser.Domain.Interfaces;

namespace CsvParser.Application
{
    public class CsvProcessor<T>(ICsvParser<T> parser) : ICsvProcessor<T>
    {
        private readonly ICsvParser<T> _parser = parser;

        public async Task<(
            List<CommonDto> Successes,
            List<(string[] Row, List<string> Errors)> Failures
        )> ProcessAsync(string filePath)
        {
            var parsedLines = await ReadAndParseLinesAsync(filePath);

            var successes = new List<T>();
            var failures = new List<(string[] Row, List<string> Errors)>();

            foreach (var row in parsedLines)
            {
                var results = await _parser.ParseAsync(new[] { row });

                foreach (var result in results)
                {
                    if (result.IsSuccess)
                        successes.Add(result.Value!);
                    else
                        failures.Add((row, result.Errors));
                }
            }

            return (successes.Select(x => ConvertToCommon(x)).ToList(), failures);
        }

        private static async Task<IEnumerable<string[]>> ReadAndParseLinesAsync(string filePath)
        {
            var lines = await CsvHelpers.ReadCsvAsync(filePath);

            // NOTE: Currently uses a hardcoded comma separator and skips the header row. 
            // In the future, consider supporting configurable separators and optional header handling.
            var parsedLines = CsvHelpers.ParseLines(lines.ToArray(), skipHeader: true, separator: ',');
            return parsedLines;
        }

        static CommonDto ConvertToCommon<T>(T value)
        {
            var dto = new CommonDto();
            foreach (var prop in typeof(T).GetProperties())
                dto.Data[prop.Name] = prop.GetValue(value);

            return dto;
        }
    }
}
