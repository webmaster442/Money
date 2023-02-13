namespace Money.Extensions;

internal static class SettingExtensions
{
    private static string ChangeExtensionIfNeeded(string fileName, string targetExtension)
    {
        string extension = Path.GetExtension(fileName).ToLower();

        if (string.IsNullOrEmpty(extension) || extension != targetExtension)
            return Path.ChangeExtension(fileName, targetExtension);

        return fileName;
    }

    public static void AppendXlsxToFileNameWhenNeeded(this ImportExportSettingsBase settings)
    {
        settings.FileName = ChangeExtensionIfNeeded(settings.FileName, ".xlsx");
    }

    public static void AppendHtmlToFileNameWhenNeeded(this ExportSetting settings)
    {
        settings.FileName = ChangeExtensionIfNeeded(settings.FileName, ".html");
    }

    public static void EnsureHasDate(this ExportSetting exportSetting)
    {
        (DateOnly firstDay, DateOnly lastDay) = DateTime.Now.GetMonthDays();

        if (exportSetting.StartDate == null)
            exportSetting.StartDate = firstDay;

        if (exportSetting.EndDate == null)
            exportSetting.EndDate = lastDay;

    }

}
