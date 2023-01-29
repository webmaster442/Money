using System.ComponentModel;

using Money.Extensions;

using Spectre.Console.Cli;

namespace Money.CommandsSettings
{
    internal sealed class StatSettings : CommandSettings
    {
        [CommandArgument(0, "[start]")]
        [Description("Start date")]
        [TypeConverter(typeof(DateonlyConverter))]
        public DateOnly StartDate { get; set; }

        [CommandArgument(0, "[end]")]
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
    }
}
