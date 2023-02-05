namespace Money.Commands
{
    internal class ManualCommand : Command
    {
        public override int Execute(CommandContext context)
        {
            var lines = Resources.readme_render.Split('\n', StringSplitOptions.TrimEntries);
            int chhunkSize = Console.WindowHeight - 2;

            foreach (var page in lines.Chunk(chhunkSize))
            {
                if (!Ui.ShowPage(page))
                    break;
            }

            return Constants.Success;
        }
    }
}
