using Spectre.Console;

namespace Money.CommandsSettings
{
    internal sealed class ImportSetting : ImportExportSettingsBase
    {
        public override ValidationResult Validate()
        {
            if (string.IsNullOrEmpty(FileName)
                || !File.Exists(FileName))
            {
                return ValidationResult.Error(Resources.ErrorFileMustExist);
            }
            return ValidationResult.Success();
        }
    }
}
