using Money.Data.Serialization;

namespace Money.Data
{
    public interface IWriteOnlyData
    {
        ulong Insert(decimal ammount, string text, DateOnly date);
        int Import(IEnumerable<SerializableSpending> toImport);
    }
}
