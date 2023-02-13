using System.Text.Json;

namespace Money.Infrastructure;
internal static class SettingsManager
{
    public static Settings Load()
    {
        var contents = File.ReadAllText(FileName);
        return 
            JsonSerializer.Deserialize<Settings>(contents, new JsonSerializerOptions
            {
                WriteIndented = true,
            })
            ?? throw new InvalidOperationException("Settings serialize error");
    }

    public static string FileName
    {
        get
        {
            var userdir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(userdir, "money.settings.json");
        }
    }

    public static void Save(Settings settings)
    {
        var contents = JsonSerializer.Serialize<Settings>(settings, new JsonSerializerOptions
        {
            WriteIndented = true,
        });
        File.WriteAllText(FileName, contents);
    }
}
