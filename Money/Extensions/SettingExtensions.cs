using System.Diagnostics.CodeAnalysis;

namespace Money.Extensions;

internal static class SettingExtensions
{
    public static void AppendXlsxToFileNameWhenNeeded(this ImportExportSettingsBase settings)
    {
        string extension = Path.GetExtension(settings.FileName).ToLower();
        if (string.IsNullOrEmpty(extension) || extension != ".xlsx")
            settings.FileName = Path.ChangeExtension(settings.FileName, ".xlsx");
    }

    public static void EnsureHasDate(this ExportSetting exportSetting)
    {
        var month = DateTime.Now.GetMonthDays();

        if (exportSetting.StartDate == null)
            exportSetting.StartDate = month.firstDay;

        if (exportSetting.EndDate == null)
            exportSetting.EndDate = month.lastDay;

    }

}
