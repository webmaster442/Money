using Spectre.Console;

namespace Money.CommandsSettings
{
    internal sealed class ImportSettings : FileCommandBase 
    {
        public override ValidationResult Validate()
        {
            if (string.IsNullOrWhiteSpace(FileName))
                return ValidationResult.Error("FileName can't be empty");

            if (!File.Exists(FileName))
                return ValidationResult.Error("FileName doesn't exist");

            return ValidationResult.Success();
        }
    }
}
