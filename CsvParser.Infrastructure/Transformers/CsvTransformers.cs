using System.Globalization;

namespace CsvParser.Infrastructure.Transformers
{
    public static class CsvTransformers
    {
        public static bool TryToInt(string input, out int value)
            => int.TryParse(input, out value);

        public static bool TryToDecimal(string input, out decimal value)
            => decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out value);

        public static (string FirstName, string LastName) SplitFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return ("", "");

            var parts = fullName.Trim().Split(' ', 2);
            return parts.Length == 2
                ? (parts[0], parts[1])
                : ("", parts[0]); // If there is only one part, treat it as the last name
        }

        public static string GetEmail(string corporateEmail, string personalEmail)
        {
            if (string.IsNullOrWhiteSpace(corporateEmail))
                return corporateEmail;
            else
                return personalEmail;
        }
    }
}
