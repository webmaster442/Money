using System.ComponentModel;

using Money.Converters;
using Money.Extensions;

using Spectre.Console;
using Spectre.Console.Cli;

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
            var (firstDay, lastDay) = DateTime.Now.GetMonthDays();
            StartDate = firstDay;
            EndDate = lastDay;
        }

        public override ValidationResult Validate()
        {
            if (StartDate > EndDate)
                return ValidationResult.Error("end date must be bigger than start date");

            return ValidationResult.Success();
        }
    }
}
