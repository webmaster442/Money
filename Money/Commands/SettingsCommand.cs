using System.Diagnostics;

using Money.Infrastructure;

namespace Money.Commands;
internal class SettingsCommand : Command
{
    private readonly ISettingsManager _settingsManager;

    public SettingsCommand(ISettingsManager settingsManager)
    {
        _settingsManager = settingsManager;
    }

    public override int Execute(CommandContext context)
    {
        if (!File.Exists(_settingsManager.FileName))
        {
            _settingsManager.Save(new Settings());
        }

        Ui.Success(Resources.SuccessSettingsOpen);
        OpenEditor(_settingsManager.FileName);
        return Constants.Success;
    }

    private static void OpenEditor(string fileName)
    {
        using (var editor = new Process())
        {
            editor.StartInfo.FileName = fileName;
            editor.StartInfo.UseShellExecute = true;
            editor.Start();
        }
    }
}
