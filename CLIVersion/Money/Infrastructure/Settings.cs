namespace Money.Infrastructure;
internal class Settings
{
    public string GitRepoPathUsedForSyncOnDisk { get; set; }

    public Settings()
    {
        GitRepoPathUsedForSyncOnDisk = string.Empty;
    }
}
