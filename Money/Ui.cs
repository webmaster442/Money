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

        public static void ExportSuccessfull(int count, string file)
        {
            AnsiConsole.MarkupLine($"[green]Successfully exported {count} items to file:\r\n {file}[/]");
        }

        public static void ImportSuccessfull(object count, string file)
        {
            AnsiConsole.MarkupLine($"[green]Successfully imported {count} items to file:\r\n {file}[/]");
        }

        public static void PrintException(IOException ex)
        {
            AnsiConsole.WriteException(ex);
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

        internal static void DetailedStats(Statistics stats)
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
    }
}
