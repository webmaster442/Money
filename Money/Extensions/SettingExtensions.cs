namespace Money.Extensions;

internal static class SettingExtensions
{
    public static void AppendXlsxToFileNameWhenNeeded(this ImportExportSettingsBase settings)
    {
        string extension = Path.GetExtension(settings.FileName).ToLower();
        if (string.IsNullOrEmpty(extension) || extension != ".xlsx")
            settings.FileName = Path.ChangeExtension(settings.FileName, ".xlsx");
    }


}
