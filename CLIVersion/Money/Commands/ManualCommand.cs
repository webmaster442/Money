namespace Money.Commands;

internal class ManualCommand : Command
{
    public override int Execute(CommandContext context)
    {
        string[] lines = Resources.ReadmeRender.Split('\n', StringSplitOptions.TrimEntries);
        int chhunkSize = Console.WindowHeight - 2;

        foreach (string[] page in lines.Chunk(chhunkSize))
        {
            if (!Ui.ShowPage(page))
                break;
        }

        return Constants.Success;
    }
}
