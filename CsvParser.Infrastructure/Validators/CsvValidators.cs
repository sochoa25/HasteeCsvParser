using System.Text.RegularExpressions;

namespace CsvParser.Infrastructure.Validators
{
    public static class CsvValidators
    {
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        private static readonly Regex PhoneRegex = new(@"^\+?[0-9\s\-().]{6,}$", RegexOptions.Compiled);

        public static bool IsValidEmail(string email)
            => !string.IsNullOrWhiteSpace(email) && EmailRegex.IsMatch(email);

        public static bool IsValidPhone(string phone)
            => !string.IsNullOrWhiteSpace(phone) && PhoneRegex.IsMatch(phone);

        public static bool HasColumns(string[] row, int expected) => row.Length >= expected;
    }
}
