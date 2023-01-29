using Money.Data.Dto;

namespace Money.Data
{
    public interface IReadonlyData
    {
        List<SerializableSpending> Export();
        Statistics GetStatistics(DateOnly start, DateOnly end);
    }
}
