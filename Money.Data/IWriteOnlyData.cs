using Money.Data.Dto;

namespace Money.Data
{
    public interface IWriteOnlyData
    {
        bool TryInsert(double ammount,
                       string text,
                       DateOnly date,
                       string category,
                       out ulong id);
        int Import(IEnumerable<SerializableSpending> toImport);
        bool TryCreateCategory(string categoryName, out ulong id);
    }
}
