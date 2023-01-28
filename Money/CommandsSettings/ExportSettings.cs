using System.ComponentModel;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Money.CommandsSettings
{
    internal sealed class ExportSettings : CommandSettings
    {
        [Description("Export file name")]
        [CommandArgument(0, "[file]")]
        public string FileName { get; init; }

        public ExportSettings()
        {
            FileName = string.Empty;
        }

        public override ValidationResult Validate()
        {
            if (string.IsNullOrWhiteSpace(FileName))
                return ValidationResult.Error("FileName can't be empty");

            return ValidationResult.Success();
        }
    }
}
