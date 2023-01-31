using System.ComponentModel;

using Spectre.Console;

namespace Money.CommandsSettings
{
    internal sealed class StatSettings : CommandSettings
    {
        [CommandArgument(0, "[start]")]
        [Description("Start date")]
        [TypeConverter(typeof(DateonlyConverter))]
        public DateOnly StartDate { get; set; }

        [CommandArgument(1, "[end]")]
        [Description("End date date")]
        [TypeConverter(typeof(DateonlyConverter))]
        public DateOnly EndDate { get; set; }

        [CommandOption("-d|--detailed")]
        public bool Detailed { get; set; }

        public StatSettings()
        {
            (DateOnly firstDay, DateOnly lastDay) = DateTime.Now.GetMonthDays();
            StartDate = firstDay;
            EndDate = lastDay;
        }

        public override ValidationResult Validate()
        {
            if (StartDate > EndDate)
                return ValidationResult.Error(Resources.ErrorDateValidate);

            return ValidationResult.Success();
        }
    }
}
