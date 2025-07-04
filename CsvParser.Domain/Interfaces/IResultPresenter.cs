using CsvParser.Domain.Models;

namespace CsvParser.Domain.Interfaces
{
    public interface IResultPresenter
    {
        Task ShowResultsAsync(ProcessorResult result);
    }
}
