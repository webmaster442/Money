using System.Reflection;

using Money.Data.Dto;

using Spectre.Console;

namespace Money.UserInterface;

internal static class Ui
{
    public static void Success(ulong id)
    {
        string hex = Convert.ToHexString(BitConverter.GetBytes(id));
        AnsiConsole.MarkupLine($"[green]{Resources.InsertSuccess} {hex}[/]");
    }

    public static void Success(string message, params object[] details)
    {
        string formatted = string.Format(message, details);
        AnsiConsole.MarkupLine($"[green]{formatted}[/]");
    }

    public static int Error(string message, params object[] details)
    {
        string formatted = string.Format(message, details);

        AnsiConsole.MarkupLine($"[red]{formatted}[/]");
        return Constants.UsageError;
    }

    public static void Warning(string message, params object[] details)
    {
        string formatted = string.Format(message, details);

        AnsiConsole.MarkupLine($"[yellow]{formatted}[/]");
    }

    public static bool Confirm(string message)
    {
        return AnsiConsole.Confirm(message, false);
    }

    public static void PrintException(Exception ex)
    {
        AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
    }

    internal static void BasicStats(Statistics stats,
                                           DateOnly startDate,
                                           DateOnly endDate)
    {

        AnsiConsole.Clear();

        string str = string.Format(Resources.StatFromToHeader, startDate, endDate);

        Rule header = new Rule($"[orange1]{str}[/]");
        AnsiConsole.Write(header);

        Table table = new Table();
        table.AddColumn("Description");
        table.AddColumn("Value");

        table.AddRow(Resources.StatSpendingCount, $"{stats.Count}");
        table.AddRow(Resources.StatSpendingWithDays, $"{stats.Days}");
        table.AddRow(Resources.StatTotalSpending, $"{stats.Sum:C}");
        table.AddRow(Resources.StatAveragePerCount, $"{stats.AvgPerCount:C}");
        table.AddRow(Resources.StatAveragePerDay, $"{stats.AvgPerDay:C}");

        AnsiConsole.Write(table);


        Table catTable = new Table();
        catTable.AddColumn(Resources.StatCategory);
        catTable.AddColumn(Resources.StatSpent);

        foreach (KeyValuePair<string, double> item in stats.SumPerCategory)
        {
            catTable.AddRow($"{item.Key}", $"{item.Value:C}");
        }
        AnsiConsole.Write(catTable);

    }

    public static void DetailedStats(Statistics stats)
    {
        Rule header = new Rule($"[orange3]{Resources.StatDayly}[/]");
        AnsiConsole.Write(header);

        Table table = new Table();
        table.AddColumn(Resources.StatDate);
        table.AddColumn(Resources.StatSumSpending);

        foreach (KeyValuePair<DateOnly, double> item in stats.SumPerDay)
        {
            table.AddRow($"{item.Key}", $"{item.Value:C}");
        }

        AnsiConsole.Write(table);
    }

    public static void PrintList<T>(IEnumerable<T> list)
    {
        Rule header = new Rule($"[green]{Resources.StatCategoryAvailable}[/]");
        AnsiConsole.Write(header);
        AnsiConsole.WriteLine();
        foreach (T? item in list)
        {
            AnsiConsole.WriteLine(item?.ToString() ?? "null");
        }
    }

    public static void PrintTable<T>(IEnumerable<T> list)
    {
        PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        Table table = new Table();
        foreach (PropertyInfo property in properties)
        {
            table.AddColumn(property.Name);
        }

        foreach (T? item in list)
        {
            string[] row = properties
                .Select(x => x.GetValue(item)?.ToString() ?? "null")
                .ToArray();

            table.AddRow(row);
        }
        AnsiConsole.Write(table);
    }

    public static bool ShowPage(string[] lines)
    {
        Console.Clear();

        foreach (string line in lines)
        {
            AnsiConsole.WriteLine(line);
        }

        AnsiConsole.WriteLine("Press a key to contine or ESC to exit");
        return Console.ReadKey().Key != ConsoleKey.Escape;
    }
}
