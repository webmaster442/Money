using System.Reflection;

using Money.Data.Dto;

using Spectre.Console;

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
        var data = _readonlyData.Find(settings.SearchTerm,
                                      settings.Category,
                                      settings.StartDate,
                                      settings.EndDate,
                                      settings.IsRegex);

        PropertyInfo[] properties = typeof(DataRowUi).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        Table table = new Table();
        foreach (PropertyInfo property in properties)
        {
            table.AddColumn(property.Name);
        }

        int pageSize = Console.WindowHeight - 6;

        await foreach (var item in data)
        {
            string[] row = properties
                .Select(x => x.GetValue(item)?.ToString() ?? "null")
                .ToArray();

            table.AddRow(row);

            if (table.Rows.Count > pageSize)
            {
                AnsiConsole.Write(table);
                table.Rows.Clear();
                AnsiConsole.WriteLine("Press a key to contine or ESC to exit");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    return Constants.Aborted;
                }
            }
        }
        AnsiConsole.Write(table);

        return Constants.Success;
    }
}
