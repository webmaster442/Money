using System.Security.Cryptography;

using Money.Data.Dto;
using Money.Data.Entities;

namespace Money.Data.DataAccess
{
    public sealed class WriteOnlyData : DataAccessBase, IWriteOnlyData
    {
        public int ChunkSize => _ChunkSize;

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

        public async Task<(bool success, ulong id)> InsertAsync(double ammount,
                                                                string text,
                                                                DateOnly date,
                                                                string category)
        {
            using MoneyContext db = ConnectDatabase();
            Category? cat = await GetCategory(db, category);

            if (cat == null)
                return (false, 0);

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
            bool exists = await GetCategory(db, categoryName) != null;

            if (exists)
                return (false, 0);

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
            Category? cat = await GetCategory(db, oldCategoryName);

            if (cat == null)
                return false;

            cat.Description = newCategoryName;

            db.Categories.Update(cat);
            return await db.SaveChangesAsync() == 1;
        }

        public async Task<(int createdCategory, int createdEntry)> ImportAsync(IEnumerable<DataRow> rows)
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
                Date = new DateOnly(row.Date.Year, row.Date.Month, row.Date.Day),
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

        public Task<int> ClearDb()
        {
            using MoneyContext db = ConnectDatabase();

            db.Spendings.RemoveRange(db.Spendings);
            db.Categories.RemoveRange(db.Categories);

            return db.SaveChangesAsync();

        }
    }
}
