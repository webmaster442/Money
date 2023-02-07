using System.Globalization;

using Money.Data.Dto;

namespace Money.Commands;

internal sealed class AddCommand : AsyncCommand<AddSettings>
{
    private readonly IWriteOnlyData _writeOnlyData;

    public AddCommand(IWriteOnlyData writeOnlyData)
    {
        _writeOnlyData = writeOnlyData;
    }

    public override Task<int> ExecuteAsync(CommandContext context, AddSettings settings)
    {
        return settings.BachMode
            ? BachMode(settings)
            : SingleMode(settings);
    }

    private UiDataRow SpendingParser(string[] parts)
    {
        return new UiDataRow
        {
            Date = DateOnly.Parse(parts[0], CultureInfo.CurrentUICulture),
            Ammount = double.Parse(parts[1], CultureInfo.CurrentUICulture),
            Description = parts[2],
            CategoryName = parts[3],
        };
    }

    private async Task<int> BachMode(AddSettings settings)
    {
        BachHandler<UiDataRow> bachHandler = new(Resources.BachSpendingsText, SpendingParser);
        IReadOnlyList<UiDataRow> bachInputs = bachHandler.DoBachInput();

        foreach (UiDataRow input in bachInputs)
        {
            (bool success, ulong id) = await _writeOnlyData.InsertAsync(input.Ammount,
                                                                        input.Description,
                                                                        input.Date,
                                                                        input.CategoryName);
            if (!success)
                Ui.Error(Resources.ErrorCategoryDoesntExist, settings.Category);
            else
                Ui.Success(id);
        }

        return Constants.Success;
    }

    private async Task<int> SingleMode(AddSettings settings)
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
