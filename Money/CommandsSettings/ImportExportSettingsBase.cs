using System.ComponentModel;

using Spectre.Console.Cli;

namespace Money.CommandsSettings
{
    internal class ImportExportSettingsBase : CommandSettings
    {
        [CommandArgument(1, "[file]")]
        [Description("file name")]
        public string FileName { get; set; }

        public ImportExportSettingsBase()
        {
            FileName = string.Empty;
        }
    }
}
