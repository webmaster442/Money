using Money.Data.Dto;

namespace Money.Data
{
    public interface IReadonlyData
    {
        IList<string> GetCategories();
        Statistics GetStatistics(DateOnly start, DateOnly end);
    }
}
