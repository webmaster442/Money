using Money.Data.Dto;

namespace Money.Data
{
    public interface IWriteOnlyData
    {
        bool TryInsert(double ammount, string text, DateOnly date, string category, out ulong id);
        bool TryCreateCategory(string categoryName, out ulong id);
        (int createdCategory, int createdEntry) Import(IList<ExportRow> rows);
        bool TryRenameCategory(string oldCategoryName, string newCategoryName);
    }
}
