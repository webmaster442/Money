using Money.Data.Serialization;

namespace Money.Data
{
    public interface IReadonlyData
    {
        List<SerializableSpending> Export();
    }
}
