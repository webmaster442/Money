namespace Money.Commands;

internal sealed class CategoryRenameCommand : AsyncCommand<CategoryRenameSettings>
{
    private readonly IWriteOnlyData _writeOnlyData;

    public CategoryRenameCommand(IWriteOnlyData writeOnlyData)
    {
        _writeOnlyData = writeOnlyData;
    }

    public override async Task<int> ExecuteAsync(CommandContext context,
                                                 CategoryRenameSettings settings)
    {
        bool result = await _writeOnlyData.RenameCategoryAsync(settings.OldCategoryName, settings.NewCategoryName);

        if (result)
        {
            Ui.Success(Resources.SuccessCategoryRename, settings.OldCategoryName, settings.NewCategoryName);
            return Constants.Success;
        }
        return Ui.Error(Resources.ErrorCategoryDoesntExist, settings.OldCategoryName);
    }
}
