using System.ComponentModel;

using Spectre.Console;

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

        [Description("Amount of money spent")]
        [CommandArgument(0, "[amount]")]
        public double Ammount { get; init; }

        [Description("Spending time. If not set current date will be used")]
        [CommandOption("-d|--date")]
        public DateOnly Date { get; init; }

        [Description("Bach input. Allows to input multiple entries at once")]
        [CommandOption("-b|--bach")]
        public bool BachMode { get; set; }

        public AddSettings()
        {
            Text = string.Empty;
            Category = string.Empty;
            Date = DateTime.Now.ToDateOnly();
        }

        public override ValidationResult Validate()
        {
            if (BachMode)
                return ValidationResult.Success();

            if (Ammount <= 0)
                return ValidationResult.Error(Resources.ErrorAddNegativeAmmount);

            if (string.IsNullOrWhiteSpace(Text))
                return ValidationResult.Error(Resources.ErrorEmptyText);

            if (string.IsNullOrWhiteSpace(Category))
                return ValidationResult.Error(Resources.ErrorCategoryNameNull);

            return ValidationResult.Success();
        }
    }
}
