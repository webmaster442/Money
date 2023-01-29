using Money.Data;

using Spectre.Console.Cli;

namespace Money.Commands
{
    internal class CategoryListCommand : Command
    {
        private readonly IReadonlyData _readonlyData;

        public CategoryListCommand(IReadonlyData readonlyData)
        {
            _readonlyData = readonlyData;
        }

        public override int Execute(CommandContext context)
        {
            Ui.PrintList(_readonlyData.GetCategories());

            return Constants.Success;
        }
    }
}
