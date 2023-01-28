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

        public static void PrintException(IOException ex)
        {
            AnsiConsole.WriteException(ex);
        }
    }
}
