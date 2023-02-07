using Money.Data.Dto;

namespace Money.Commands;

internal sealed class FindCommand : AsyncCommand<FindSettings>
{
    private readonly IReadonlyData _readonlyData;

    public FindCommand(IReadonlyData readonlyData)
    {
        _readonlyData = readonlyData;
    }

    public override async Task<int> ExecuteAsync(CommandContext context,
                                           FindSettings settings)
    {
        List<UiDataRow> data = await _readonlyData.Find(settings.SearchTerm,
                                                        settings.Category,
                                                        settings.StartDate,
                                                        settings.EndDate,
                                                        settings.IsRegex);

        Ui.PrintTable(data);
        return Constants.Success;
    }
}
