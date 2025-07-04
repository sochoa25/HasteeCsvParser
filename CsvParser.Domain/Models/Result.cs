namespace CsvParser.Domain.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public List<string> Errors { get; }

        private Result(T value)
        {
            IsSuccess = true;
            Value = value;
            Errors = new List<string>();
        }

        private Result(List<string> errors)
        {
            IsSuccess = false;
            Value = default;
            Errors = errors;
        }

        public static Result<T> Success(T value) => new(value);

        public static Result<T> Failure(params string[] errors) => new(errors.ToList());

        public static Result<T> Failure(IEnumerable<string> errors) => new(errors.ToList());
    }
}