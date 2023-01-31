namespace Money.Commands
{
    internal sealed class CategoryListCommand : AsyncCommand
    {
        private readonly IReadonlyData _readonlyData;

        public CategoryListCommand(IReadonlyData readonlyData)
        {
            _readonlyData = readonlyData;
        }

        public override async Task<int> ExecuteAsync(CommandContext context)
        {
            var categories = await _readonlyData.GetCategoriesAsync();

            Ui.PrintList(categories);

            return Constants.Success;
        }
    }
}
