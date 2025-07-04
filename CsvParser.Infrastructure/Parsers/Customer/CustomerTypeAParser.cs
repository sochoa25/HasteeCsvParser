using CsvParser.Domain.DTOs;
using CsvParser.Domain.Interfaces;
using CsvParser.Domain.Models;
using CsvParser.Infrastructure.Transformers;
using CsvParser.Infrastructure.Validators;

namespace CsvParser.Infrastructure.Parsers.Customer
{
    public class CustomerTypeAParser : ICsvParser<CustomerDto>
    {
        public CustomerTypeAParser()
        {
        }

        public async Task<IEnumerable<Result<CustomerDto>>> ParseAsync(IEnumerable<string[]> rows)
        {
            var results = new List<Result<CustomerDto>>();

            foreach (var row in rows)
            {
                if (!CsvValidators.HasColumns(row, 5))
                {
                    results.Add(Result<CustomerDto>.Failure("Row has fewer than 5 columns"));
                    continue;
                }

                var strCustomerId = row[0];
                var fullName = row[1];
                var email = row[2];
                var phone = row[3];
                var strSalary = row[4];

                var errors = new List<string>();
                var dto = new CustomerDto();

                // Transformers
                if (!CsvTransformers.TryToInt(strCustomerId, out var id))
                    errors.Add("Invalid ID");
                dto.Id = id;

                if (!CsvTransformers.TryToDecimal(strSalary, out var salary))
                    errors.Add("Invalid Salary");
                dto.Salary = salary;

                var (first, last) = CsvTransformers.SplitFullName(fullName);
                dto.Name = first;
                dto.Surname = last;

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
