namespace Money.Tests;

internal sealed class DataFiles
{
    public string ImportData1000 { get; }

    public DataFiles()
    {
        string folder = Path.Combine(AppContext.BaseDirectory, "Data");

        ImportData1000 = Path.Combine(folder, "ImportData1000.xlsx");
    }
}
