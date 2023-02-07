using System.ComponentModel;

using Spectre.Console;

namespace Money.CommandsSettings;

internal sealed class ExportSetting : ImportExportSettingsBase
{
    [CommandArgument(1, "[start]")]
    [Description("Start date (optional)")]
    [TypeConverter(typeof(NullableDateonlyConverter))]
    public DateOnly? StartDate { get; set; }

    [CommandArgument(2, "[end]")]
    [Description("End date (optional)")]
    [TypeConverter(typeof(NullableDateonlyConverter))]
    public DateOnly? EndDate { get; set; }

    public override ValidationResult Validate()
    {
        if (EndDate != null
            && StartDate != null
            && StartDate.Value > EndDate.Value)
        {
            return ValidationResult.Error(Resources.ErrorDateValidate);
        }

        if (string.IsNullOrEmpty(FileName))
        {
            return ValidationResult.Error(Resources.ErrorEmptyFileName);
        }

        return ValidationResult.Success();
    }
}
