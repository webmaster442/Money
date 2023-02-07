namespace Money.Commands
{
    internal sealed class CategoryAddCommand : AsyncCommand<CategorySettings>
    {
        private readonly IWriteOnlyData _writeOnlyData;

        public CategoryAddCommand(IWriteOnlyData writeOnlyData)
        {
            _writeOnlyData = writeOnlyData;
        }

        public override async Task<int> ExecuteAsync(CommandContext context,
                                               CategorySettings settings)
        {
            (bool success, ulong id) = await _writeOnlyData.CreateCategoryAsync(settings.CategoryName);

            if (!success)
                return Ui.Error(Resources.ErrorCategoryAllreadyExists, settings.CategoryName);

            Ui.Success(id);

            return Constants.Success;
        }
    }
}
