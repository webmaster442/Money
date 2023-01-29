using Money.Data;

using Spectre.Console;

namespace Money
{
    internal static class Ui
    {
        public static void Inserted(ulong id)
        {
            string hex = Convert.ToHexString(BitConverter.GetBytes(id));
            AnsiConsole.MarkupLine($"[green]Successfully inserted with id: {hex}[/]");
        }

        internal static void BasicStats(Statistics stats,
                                               DateOnly startDate,
                                               DateOnly endDate)
        {

            AnsiConsole.Clear();

            var header = new Rule($"[orange1]Spendings {startDate} {endDate}[/]");
            AnsiConsole.Write(header);

            var table = new Table();
            table.AddColumn("Description");
            table.AddColumn("Value");

            table.AddRow("Spending count", $"{stats.Count}");
            table.AddRow("Spendings with days", $"{stats.Days}");
            table.AddRow("Total spending", $"{stats.Sum:C}");
            table.AddRow("Average / count", $"{stats.AvgPerCount:C}");
            table.AddRow("Average / day", $"{stats.AvgPerDay:C}");

            AnsiConsole.Write(table);
        }

        public static void DetailedStats(Statistics stats)
        {
            var header = new Rule($"[orange3]Dayly breakdown[/]");
            AnsiConsole.Write(header);

            var table = new Table();
            table.AddColumn("Date");
            table.AddColumn("Sum spending");

            foreach (var item in stats.SumPerDay)
            {
                table.AddRow($"{item.Key}", $"{item.Value:C}");
            }

            AnsiConsole.Write(table);
        }

        public static int Error(string message)
        {
            AnsiConsole.MarkupLine($"[red]{message}[/]");
            return Constants.UsageError;
        }

        public static void PrintList<T>(IEnumerable<T> list)
        {
            var header = new Rule($"[green]Available categories[/]");
            AnsiConsole.Write(header);
            AnsiConsole.WriteLine();
            foreach (var item in list)
            {
                AnsiConsole.WriteLine(item?.ToString() ?? "null");
            }
        }
    }
}
