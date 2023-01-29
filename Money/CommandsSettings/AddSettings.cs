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

        [Description("Spending category")]
        [CommandOption("-c|--category")]
        public string Category { get; set; }

        [Description("Ammount of money spent")]
        [CommandArgument(0, "[amount]")]
        public double Ammount { get; init; }

        [Description("Spending time. If not set current date & time will be used")]
        [CommandOption("-d|--date")]
        public DateOnly Date { get; init; }

        public AddSettings()
        {
            Text = string.Empty;
            Category = string.Empty;
            Date = DateTime.Now.ToDateOnly();
        }

        public override ValidationResult Validate()
        {
            if (Ammount <= 0)
                return ValidationResult.Error("Amount must be positive and greater than 0");

            if (string.IsNullOrWhiteSpace(Text))
                return ValidationResult.Error("Text can't be empty");

            if (string.IsNullOrWhiteSpace(Category))
                return ValidationResult.Error("Category can't be empty");

            return ValidationResult.Success();
        }
    }
}
