using CsvParser.Domain.Models;

namespace CsvParser.Domain.Interfaces
{
    public interface ICustomerApi
    {
        Task<Result<string>> GetPhoneAsync(int id);
    }
}