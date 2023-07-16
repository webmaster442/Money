namespace Money.Commands;

internal sealed class CategoryAddCommand : AsyncCommand<CategoryAddSettings>
{
    private readonly IWriteOnlyData _writeOnlyData;

    public CategoryAddCommand(IWriteOnlyData writeOnlyData)
    {
        _writeOnlyData = writeOnlyData;
    }

    public override Task<int> ExecuteAsync(CommandContext context,
                                           CategoryAddSettings settings)
    {
        return settings.BachMode
            ? BachMode(settings)
            : SingleMode(settings);
    }

    private async Task<int> BachMode(CategoryAddSettings settings)
    {
        BachHandler<string> bachHandler = new(Resources.BachCategoryText, parts => parts[0]);
        IReadOnlyList<string> bachInputs = bachHandler.DoBachInput();
        foreach (string input in bachInputs)
        {
            (bool success, ulong id) = await _writeOnlyData.CreateCategoryAsync(input);

            if (!success)
                Ui.Error(Resources.ErrorCategoryAllreadyExists, settings.CategoryName);
            else
                Ui.Success(id);
        }

        return Constants.Success;
    }

    private async Task<int> SingleMode(CategoryAddSettings settings)
    {
        (bool success, ulong id) = await _writeOnlyData.CreateCategoryAsync(settings.CategoryName);

        if (!success)
            return Ui.Error(Resources.ErrorCategoryAllreadyExists, settings.CategoryName);

        Ui.Success(id);

        return Constants.Success;
    }
}
