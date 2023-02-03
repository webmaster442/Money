using System.Security.Cryptography;

using Microsoft.EntityFrameworkCore;

using Money.Data.Dto;
using Money.Data.Entities;

namespace Money.Data.DataAccess
{
    public sealed class WriteOnlyData : DataAccessBase, IWriteOnlyData
    {
        public WriteOnlyData(IDatabaseFileLocator databaseLocator) : base(databaseLocator)
        {
        }

        private static ulong CreateId(long currentTime)
        {
            byte[] buffer = BitConverter.GetBytes(currentTime);
            byte[] random = RandomNumberGenerator.GetBytes(buffer.Length);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] ^= random[i];
            }
            return BitConverter.ToUInt64(buffer);

        }

        public async Task<(bool success, ulong id)> InsertAsync(double ammount, string text, DateOnly date, string category)
        {
            using MoneyContext db = ConnectDatabase();
            Category? cat = await db
                .Categories
                .Where(c => c.Description == category.ToLower())
                .FirstOrDefaultAsync();

            if (cat == null)
            {
                return (false, 0);
            }

            Spending entity = new Spending
            {
                Id = CreateId(DateTime.UtcNow.ToBinary()),
                Date = date,
                Description = text,
                Ammount = ammount,
                AddedOn = DateTime.Now,
                Category = cat,
            };

            db.Spendings.Add(entity);

            bool result = await db.SaveChangesAsync() == 1;

            return (result, entity.Id);
        }

        public async Task<(bool success, ulong id)> CreateCategoryAsync(string categoryName)
        {
            using MoneyContext db = ConnectDatabase();
            bool exists = await db 
                .Categories
                .Where(c => c.Description == categoryName.ToLower())
                .AnyAsync();

            if (exists)
            {
                return (false, 0);
            }

            Category toInsert = new Category
            {
                Id = CreateId(DateTime.Now.ToBinary()),
                Description = categoryName.ToLower(),
            };

            db.Categories.Add(toInsert);

            bool result = await db.SaveChangesAsync() == 1;

            return (result, toInsert.Id);
        }

        public async Task<bool> RenameCategoryAsync(string oldCategoryName, string newCategoryName)
        {
            using MoneyContext db = ConnectDatabase();
            Category? cat = db
                .Categories
                .Where(c => c.Description == oldCategoryName.ToLower())
                .FirstOrDefault();

            if (cat == null)
                return false;

            cat.Description = newCategoryName;

            db.Categories.Update(cat);
            return await db.SaveChangesAsync() == 1;
        }

        public async Task<(int createdCategory, int createdEntry)> ImportAsync(IEnumerable<ExportRow> rows)
        {
            int createdCategory = 0;
            foreach (string? category in rows.Select(x => x.CategoryName.ToLower()).Distinct())
            {
                var result = await CreateCategoryAsync(category);

                if (result.success)
                    ++createdCategory;
            }

            using MoneyContext db = ConnectDatabase();

            Dictionary<string, Category> categories = db.Categories.ToDictionary(x => x.Description, x => x);

            IEnumerable<Spending> bulk = rows.Select(row => new Spending
            {
                Date = row.Date,
                Description = row.Description,
                AddedOn = row.AddedOn,
                Ammount = row.Ammount,
                Category = categories[row.CategoryName.ToLower()],
                Id = CreateId(DateTime.UtcNow.ToBinary()),
            });

            db.Spendings.AddRange(bulk);
            int createdEntry = await db.SaveChangesAsync();

            return (createdCategory, createdEntry);
        }
    }
}
