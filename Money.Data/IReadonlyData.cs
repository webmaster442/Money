using Money.Data.Dto;

namespace Money.Data
{
    public interface IReadonlyData
    {
        Task<List<string>> GetCategoriesAsync();
        Task<Statistics> GetStatisticsAsync(DateOnly start, DateOnly end);
        Task<List<ExportRow>> ExportAsync(DateOnly? start = null, DateOnly? end = null);
    }
}
