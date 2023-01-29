using Money.Data.Dto;

namespace Money.Data
{
    public interface IReadonlyData
    {
        Statistics GetStatistics(DateOnly start, DateOnly end);
    }
}
