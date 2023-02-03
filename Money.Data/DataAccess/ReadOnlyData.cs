
using Microsoft.EntityFrameworkCore;

using Money.Data.Dto;
using Money.Data.Entities;

namespace Money.Data.DataAccess
{
    public sealed class ReadOnlyData : DataAccessBase, IReadonlyData
    {
        int IReadonlyData.ChunkSize => _ChunkSize;

        public ReadOnlyData(IDatabaseFileLocator databaseLocator) : base(databaseLocator)
        {
        }

        public Task<List<DataRow>> ExportAsync(DateOnly? start = null, DateOnly? end = null)
        {
            using MoneyContext db = ConnectDatabase();
            IQueryable<Spending> query = db
                .Spendings
                .Include(s => s.Category)
                .AsQueryable();

            if (start != null)
                query = query.Where(x => x.Date >= start);

            if (end != null)
                query = query.Where(x => x.Date <= end);

            return query.Select(spending => new DataRow
            {
                Date = spending.Date.ToDateTime(TimeOnly.MinValue),
                Description = spending.Description,
                AddedOn = spending.AddedOn,
                Ammount = spending.Ammount,
                CategoryName = spending.Category.Description
            }).ToListAsync();
        }

        public Task<List<DataRow>> ExportBackupAsync(int startOffset)
        {
            using MoneyContext db = ConnectDatabase();
            var query = db
                .Spendings
                .Include(s => s.Category)
                .Skip(startOffset)
                .Take(_ChunkSize);

            return query.Select(spending => new DataRow
            {
                Date = spending.Date.ToDateTime(TimeOnly.MinValue),
                Description = spending.Description,
                AddedOn = spending.AddedOn,
                Ammount = spending.Ammount,
                CategoryName = spending.Category.Description
            }).ToListAsync();
        }

        public Task<List<string>> GetCategoriesAsync()
        {
            using MoneyContext db = ConnectDatabase();
            return db
                .Categories
                .Select(c => c.Description)
                .ToListAsync();
        }

        public async Task<Statistics> GetStatisticsAsync(DateOnly start, DateOnly end)
        {
            using MoneyContext db = ConnectDatabase();
            List<Spending> data = await db
                .Spendings
                .Include(s => s.Category)
                .Where(x => x.Date >= start)
                .Where(x => x.Date <= end)
                .ToListAsync();

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

        public Task<int> GetSpendingsCount()
        {
            using MoneyContext db = ConnectDatabase();
            return GetSpendingsCount(db);
        }
    }
}
