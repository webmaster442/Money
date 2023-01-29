using Spectre.Console;

namespace Money.CommandsSettings
{
    internal sealed class CreateExcelTemplateSettings : ImportExportSettingsBase
    {
        public override ValidationResult Validate()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                return ValidationResult.Error("file name can't be empty");
            }

            return ValidationResult.Success();
        }
    }
}
