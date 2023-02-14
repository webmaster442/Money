using Money.Infrastructure;

using Spectre.Console;

namespace Money.Extensions;
internal static class SettingsExtensions
{
    public static ValidationResult Validate(this Settings settings)
    {
        if (string.IsNullOrEmpty(settings.GitRepoPathUsedForSyncOnDisk))
            return ValidationResult.Error(Resources.ErrorFolderNotSet);

        if (!Directory.Exists(settings.GitRepoPathUsedForSyncOnDisk))
            return ValidationResult.Error(Resources.ErrorFolderDoesntExist);

        var gitDir = Path.Combine(settings.GitRepoPathUsedForSyncOnDisk, ".git");

        if (!Directory.Exists(gitDir))
            return ValidationResult.Error(Resources.ErrorFolderNotGitFolder);

        return ValidationResult.Success();
    }
}
