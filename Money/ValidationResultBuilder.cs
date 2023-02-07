using System.Text;

using Spectre.Console;

namespace Money
{
    internal sealed class ValidationResultBuilder
    {
        private readonly StringBuilder _errorString;

        public ValidationResultBuilder()
        {
            _errorString = new StringBuilder();
        }

        public ValidationResultBuilder AddIfFalse(Func<bool> predicate, string message)
        {
            if (!predicate.Invoke())
                _errorString.AppendLine(message);

            return this;
        }

        public ValidationResultBuilder AddIfTrue(Func<bool> predicate, string message)
        {
            if (predicate.Invoke())
                _errorString.AppendLine(message);

            return this;
        }

        public ValidationResultBuilder AddIfNullOrEmpty(string str, string errorMessage)
        {
            if (string.IsNullOrEmpty(str))
                _errorString.AppendLine(errorMessage);

            return this;
        }

        public ValidationResultBuilder AddIfNullOrWhiteSpace(string str, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(str))
                _errorString.AppendLine(errorMessage);

            return this;
        }

        public ValidationResult Build()
        {
            if (_errorString.Length < 1)
                return ValidationResult.Success();

            return ValidationResult.Error(_errorString.ToString());
        }
    }
}
