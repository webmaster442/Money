using System.Diagnostics;

using Money.Infrastructure;

namespace Money.Commands;
internal class SettingsCommand : Command
{
    public override int Execute(CommandContext context)
    {
        if (!File.Exists(SettingsManager.FileName))
        {
            SettingsManager.Save(new Settings());
        }

        Ui.Success(Resources.SuccessSettingsOpen);
        OpenEditor(SettingsManager.FileName);
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
