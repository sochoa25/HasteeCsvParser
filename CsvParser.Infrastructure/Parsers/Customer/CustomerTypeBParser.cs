using CsvParser.Domain.DTOs;
using CsvParser.Domain.Interfaces;
using CsvParser.Domain.Models;
using CsvParser.Infrastructure.Transformers;
using CsvParser.Infrastructure.Validators;

namespace CsvParser.Infrastructure.Parsers.Customer
{
    public class CustomerTypeBParser : ICsvParser<CustomerDto>
    {
        private readonly ICustomerApi _customerApi;

        public CustomerTypeBParser(ICustomerApi customerApi)
        {
            _customerApi = customerApi;
        }

        public async Task<IEnumerable<Result<CustomerDto>>> ParseAsync(IEnumerable<string[]> rows)
        {
            var results = new List<Result<CustomerDto>>();

            foreach (var row in rows)
            {
                if (!CsvValidators.HasColumns(row, 6))
                {
                    results.Add(Result<CustomerDto>.Failure("Row has fewer than 6 columns"));
                    continue;
                }

                var strId = row[0];
                var name = row[1];
                var surname = row[2];
                var corporateEmail = row[3];
                var personalEmail = row[4];
                var strSalary = row[5];

                var errors = new List<string>();
                var dto = new CustomerDto();

                // Transformers
                if (!CsvTransformers.TryToInt(strId, out var id))
                    errors.Add("Invalid ID");
                dto.Id = id;

                if (!CsvTransformers.TryToDecimal(strSalary, out var salary))
                    errors.Add("Invalid Salary");
                dto.Salary = salary;

                dto.Name = name;
                dto.Surname = surname;

                var email = CsvTransformers.GetEmail(corporateEmail, personalEmail);

                // Fetch from external API
                var phoneResult = await _customerApi.GetPhoneAsync(id);
                
                string phone = string.Empty;
                if (phoneResult.IsSuccess)
                {
                    phone = phoneResult.Value;
                }
                else
                {
                    errors.Add($"Failed to fetch email: {phoneResult.Errors.FirstOrDefault()}");
                }

                // Validators
                if (!CsvValidators.IsValidEmail(email))
                    errors.Add("Invalid Email");
                dto.Email = email;

                if (!CsvValidators.IsValidPhone(phone))
                    errors.Add("Invalid Phone");
                dto.Phone = phone;


                results.Add(errors.Any()
                    ? Result<CustomerDto>.Failure(errors)
                    : Result<CustomerDto>.Success(dto));
            }

            return await Task.FromResult(results);
        }
    }
}
