using Money.Data.Dto;

namespace Money.Data.DataAccess
{
    public class ReadOnlyData : IReadonlyData
    {

        public ReadOnlyData()
        {
        }

        private static MoneyContext ConnectDatabase()
        {
            return new MoneyContext();
        }

        public Statistics GetStatistics(DateOnly start, DateOnly end)
        {
            using var db = ConnectDatabase();
            var data = db
                .Spendings
                .Where(x => x.Date >= start)
                .Where(x => x.Date <= end)
                .ToList();

            var dates = data
                .GroupBy(x => x.Date)
                .ToDictionary(x => x.Key, x => x.Sum(x => x.Ammount));

            return new Statistics
            {
                SumPerDay = dates,
                Count = data.Count,
            };
        }
    }
}
