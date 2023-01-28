using System.ComponentModel;

using Money.Extensions;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Money.CommandsSettings
{
    internal sealed class AddSettings : CommandSettings
    {
        [Description("A short text description of the spending")]
        [CommandOption("-t|--text")]
        public string Text { get; init; }

        [Description("Ammount of money spent")]
        [CommandArgument(0, "[amount]")]
        public decimal Ammount { get; init; }

        [Description("Spending time")]
        [CommandOption("-d|--date")]
        public DateOnly Date { get; init; }

        public AddSettings()
        {
            Text = string.Empty;
            Date = DateTime.Now.ToDateOnly();
        }

        public override ValidationResult Validate()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return ValidationResult.Error("Text can't be empty");

            if (Ammount < 0)
                return ValidationResult.Error("Amount must be positive");

            return ValidationResult.Success();
        }
    }
}
