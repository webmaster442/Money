using System.Runtime.InteropServices;

namespace Money.Data;

public sealed class DatabaseFileLocator : IDatabaseFileLocator
{
    private const string FileName = "money.database.db";
    private const string OneDriveConsumer = "%OneDriveConsumer%";
    private const string OneDriveCommercial = "%OneDriveCommercial%";

    public string DatabasePath { get; }

    public DatabaseFileLocator()
    {
        DatabasePath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? GetPathWindows()
            : GetPathDefault();
    }

    private static string GetPathDefault()
    {
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        return Path.Combine(folder, FileName);
    }

    private static string GetPathWindows()
    {
        string oneDrive = Environment.ExpandEnvironmentVariables(OneDriveCommercial);

        if (string.IsNullOrEmpty(oneDrive) || oneDrive == OneDriveCommercial)
            oneDrive = Environment.ExpandEnvironmentVariables(OneDriveConsumer);

        return string.IsNullOrEmpty(oneDrive) || oneDrive == OneDriveConsumer
            ? GetPathDefault()
            : Path.Combine(oneDrive, FileName);
    }
}
