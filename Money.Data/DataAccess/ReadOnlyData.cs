
using Microsoft.EntityFrameworkCore;

using Money.Data.Dto;
using Money.Data.Entities;

namespace Money.Data.DataAccess
{
    public class ReadOnlyData : IReadonlyData
    {
        private static MoneyContext ConnectDatabase()
        {
            return new MoneyContext();
        }

        public IList<ExcelTableRow> ExcelExport(DateOnly? start = null, DateOnly? end = null)
        {
            using MoneyContext db = ConnectDatabase();
            var query = db
                .Spendings
                .Include(s => s.Category)
                .AsQueryable();

            if (start != null)
                query = query.Where(x => x.Date >= start);

            if (end != null)
                query = query.Where(x => x.Date <= end);

            return query.Select(spending => new ExcelTableRow
            {
                Date = spending.Date,
                Description= spending.Description,
                AddedOn= spending.AddedOn,
                Ammount= spending.Ammount,
                CategoryName = spending.Category.Description
            }).ToList();
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
            List<Spending> data = db
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
