
using Microsoft.EntityFrameworkCore;

using Money.Data.Dto;
using Money.Data.Entities;

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

        public IList<string> GetCategories()
        {
            using MoneyContext db = ConnectDatabase();
            return db
                .Categories
                .Select(c => c.Description)
                .ToList();
        }

        public Statistics GetStatistics(DateOnly start, DateOnly end)
        {
            using MoneyContext db = ConnectDatabase();
            List<Entities.Spending> data = db
                .Spendings
                .Include(s => s.Category)
                .Where(x => x.Date >= start)
                .Where(x => x.Date <= end)
                .ToList();

            Dictionary<DateOnly, double> dates = data
                .GroupBy(x => x.Date)
                .ToDictionary(x => x.Key, x => x.Sum(x => x.Ammount));

            Dictionary<string, double> categoreis = data
                .GroupBy(x => x.Category.Description)
                .ToDictionary(x => x.Key, x => x.Sum(x => x.Ammount));

            return new Statistics
            {
                SumPerDay = dates,
                Count = data.Count,
                SumPerCategory = categoreis,
            };
        }
    }
}
