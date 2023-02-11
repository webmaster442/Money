using Money.Data.Dto;

namespace Money.Commands;
internal class GenTestDataCommand : AsyncCommand
{
    private readonly IWriteOnlyData _writeOnlyData;

    private readonly string[] _categories;

    public GenTestDataCommand(IWriteOnlyData writeOnlyData)
    {
        _writeOnlyData = writeOnlyData;
        _categories = new string[]
        {
            "Food",
            "Transport",
            "Car",
            "Games",
            "Rent",
        };
    }

    private static DateTime RandomDate(DateTime now)
    {
        int year = Random.Shared.Next(now.Year - 2, now.Year + 1);
        int month = Random.Shared.Next(1, 13);
        int day = Random.Shared.Next(1, 28);
        return new DateTime(year, month, day);
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        await _writeOnlyData.ClearDb();

        const int count = 100_000;

        var rows = new List<DataRow>(count);
        for (int i=0; i< count; i++)
        {
            rows.Add(new DataRow
            {
                Date = RandomDate(DateTime.Now),
                AddedOn = DateTime.Now,
                Ammount = Random.Shared.Next(1000, 250_000),
                CategoryName = _categories[Random.Shared.Next(0, _categories.Length)],
                Description = $"Spending item {i}",
            }) ;
        }

        var result = await _writeOnlyData.ImportAsync(rows);

        Ui.Success(Resources.SuccesImport, result.createdCategory, result.createdEntry);
        return Constants.Success;
    }
}
