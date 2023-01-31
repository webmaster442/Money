using System.ComponentModel;

namespace Money.CommandsSettings
{
    internal abstract class ImportExportSettingsBase : CommandSettings
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
