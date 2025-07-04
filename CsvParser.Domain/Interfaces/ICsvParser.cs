using CsvParser.Domain.Models;

namespace CsvParser.Domain.Interfaces
{
    public interface ICsvParser<T>
    {
        Task<IEnumerable<Result<T>>> ParseAsync(IEnumerable<string[]> rows);
    }
}
