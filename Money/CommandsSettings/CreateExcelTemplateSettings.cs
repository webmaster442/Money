using Money.Properties;

using Spectre.Console;

namespace Money.CommandsSettings
{
    internal sealed class CreateExcelTemplateSettings : ImportExportSettingsBase
    {
        public override ValidationResult Validate()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                return ValidationResult.Error(Resources.ErrorEmptyFileName);
            }

            return ValidationResult.Success();
        }
    }
}
