using System.ComponentModel;

using Money.Converters;

using Spectre.Console.Cli;

namespace Money.CommandsSettings
{
    internal class ImportExportSettingsBase : CommandSettings
    {
        [CommandArgument(1, "[file]")]
        [Description("file name")]
        public string FileName { get; set; }

        [CommandArgument(1, "[start]")]
        [Description("Start date")]
        [TypeConverter(typeof(NullableDateonlyConverter))]
        public DateOnly? StartDate { get; set; }

        [CommandArgument(2, "[end]")]
        [Description("End date date")]
        [TypeConverter(typeof(NullableDateonlyConverter))]
        public DateOnly? EndDate { get; set; }

        public ImportExportSettingsBase()
        {
            FileName = string.Empty;
        }
    }
}
