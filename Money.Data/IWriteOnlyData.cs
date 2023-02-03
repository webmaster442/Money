using Money.Data.Dto;

namespace Money.Data
{
    public interface IWriteOnlyData
    {
        Task<(bool success, ulong id)> InsertAsync(double ammount, string text, DateOnly date, string category);
        Task<(bool success, ulong id)> CreateCategoryAsync(string categoryName);
        Task<bool> RenameCategoryAsync(string oldCategoryName, string newCategoryName);
        Task<(int createdCategory, int createdEntry)> ImportAsync(IEnumerable<DataRow> rows);
    }
}
