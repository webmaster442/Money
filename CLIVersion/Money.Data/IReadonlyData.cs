using Money.Data.Dto;

namespace Money.Data;

public interface IReadonlyData
{
    int ChunkSize { get; }

    Task<List<string>> GetCategoriesAsync();
    Task<Statistics> GetStatisticsAsync(DateOnly start, DateOnly end);
    Task<List<DataRowExcel>> ExportAsync(DateOnly? start = null, DateOnly? end = null);
    IAsyncEnumerable<DataRowBackup> ExportBackupAsync();
    IAsyncEnumerable<DataRowBackup> ExportBackupAsync(DateTime startDate);
    Task<int> GetSpendingsCount();
    IAsyncEnumerable<DataRowUi> Find(string what,
                                     string? category,
                                     DateOnly? startDate,
                                     DateOnly? endDate,
                                     bool isRegex);

    Task<DateTime> GetLastInsertDate();
    Task<DateTime> GetFirstInsertDate();
}
