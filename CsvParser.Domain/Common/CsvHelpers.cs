namespace CsvParser.Domain.Common
{
    public static class CsvHelpers
    {

        public static async Task<string[]> ReadCsvAsync(string csvPath)
        {
            try
            {
                if (!File.Exists(csvPath))
                {
                    throw new FileNotFoundException($"The file '{csvPath}' does not exist.");
                }

                var lines = await File.ReadAllLinesAsync(csvPath);

                return lines;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading the CSV file: {ex.Message}", ex);
            }
        }

        public static IEnumerable<string[]> ParseLines(string[] lines, bool skipHeader = true, char separator = ',')
        {
            var effectiveLines = skipHeader ? lines.Skip(1) : lines;

            foreach (var line in effectiveLines)
            {
                var parts = line.Split(separator);

                // Trim each field
                yield return parts.Select(p => p.Trim()).ToArray();
            }
        }
    }
}