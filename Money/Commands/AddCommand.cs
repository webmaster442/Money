namespace Money.Commands
{
    internal sealed class AddCommand : AsyncCommand<AddSettings>
    {
        private readonly IWriteOnlyData _writeOnlyData;

        public AddCommand(IWriteOnlyData writeOnlyData)
        {
            _writeOnlyData = writeOnlyData;
        }

        public override async Task<int> ExecuteAsync(CommandContext context,
                                                     AddSettings settings)
        {
            (bool success, ulong id) = await _writeOnlyData.InsertAsync(settings.Ammount,
                                                          settings.Text,
                                                          settings.Date,
                                                          settings.Category);

            if (!success)
            {
                return Ui.Error(Resources.ErrorCategoryDoesntExist, settings.Category);
            }

            Ui.Success(id);
            return Constants.Success;
        }
    }
}
