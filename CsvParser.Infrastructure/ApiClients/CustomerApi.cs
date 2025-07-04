using CsvParser.Domain.Interfaces;
using CsvParser.Domain.Models;

namespace CsvParser.Infrastructure.ApiClients
{
    public class CustomerApi : ICustomerApi
    {
        public async Task<Result<string>> GetPhoneAsync(int id)
        {
            try
            {
                // TODO: implement real API call
                await Task.Delay(50);
                return Result<string>.Success($"555-555-555");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure([ex.Message]);
            }
            
        }
    }

}
