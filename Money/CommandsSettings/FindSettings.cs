using System.ComponentModel;

using Spectre.Console;

namespace Money.CommandsSettings
{
    internal sealed class FindSettings : CommandSettings
    {
        [CommandOption("-r|--regex")]
        public bool IsRegex { get; set; }

        [CommandOption("-s|--startdate")]
        [Description("Start date (optional)")]
        [TypeConverter(typeof(NullableDateonlyConverter))]
        public DateOnly? StartDate { get; set; }

        [CommandOption("-e|--enddate")]
        [Description("End date (optional)")]
        [TypeConverter(typeof(NullableDateonlyConverter))]
        public DateOnly? EndDate { get; set; }

        [Description("Spending category. When set, only searches in given category")]
        [CommandOption("-c|--category")]
        public string Category { get; set; }

        [Description("Search term")]
        [CommandArgument(0, "[term]")]
        public string SearchTerm { get; set; }

        public FindSettings()
        {
            Category = string.Empty;
            SearchTerm = string.Empty;
        }

        public override ValidationResult Validate()
        {
            if (EndDate != null
                && StartDate != null
                && StartDate.Value > EndDate.Value)
            {
                return ValidationResult.Error(Resources.ErrorDateValidate);
            }

            if (string.IsNullOrEmpty(SearchTerm))
                return ValidationResult.Error(Resources.ErrorNoSearchTermGiven);

            return ValidationResult.Success();
        }
    }
}
