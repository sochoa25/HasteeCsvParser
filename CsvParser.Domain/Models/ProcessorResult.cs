using CsvParser.Domain.DTOs;

namespace CsvParser.Domain.Models
{
    public class ProcessorResult
    {
        public Type DtoType { get; set; } = null!;
        public List<CommonDto> Successes { get; set; } = new();
        public List<(string[] Row, List<string> Errors)> Failures { get; set; } = new();
    }
}
