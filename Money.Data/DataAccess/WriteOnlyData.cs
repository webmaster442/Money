using System.Security.Cryptography;

using Money.Data.Dto;
using Money.Data.Entities;

namespace Money.Data.DataAccess
{
    public sealed class WriteOnlyData : IWriteOnlyData
    {
        public WriteOnlyData()
        {
        }

        private static MoneyContext ConnectDatabase()
        {
            return new MoneyContext();
        }

        private ulong CreateId(long currentTime)
        {
            byte[] buffer = BitConverter.GetBytes(currentTime);
            byte[] random = RandomNumberGenerator.GetBytes(buffer.Length);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] ^= random[i];
            }
            return BitConverter.ToUInt64(buffer);

        }

        public bool TryInsert(double ammount,
                            string text,
                            DateOnly date,
                            string category,
                            out ulong id)
        {
            using MoneyContext db = ConnectDatabase();
            Category? cat = db
                .Categories
                .Where(c => c.Description == category.ToLower())
                .FirstOrDefault();

            if (cat == null)
            {
                id = 0;
                return false;
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

            id = entity.Id;
            return db.SaveChanges() == 1;
        }

        public bool TryCreateCategory(string categoryName, out ulong Id)
        {
            using MoneyContext db = ConnectDatabase();
            bool exists = db
                .Categories
                .Where(c => c.Description == categoryName.ToLower())
                .Any();

            if (exists)
            {
                Id = 0;
                return false;
            }

            Category toInsert = new Category
            {
                Id = CreateId(DateTime.Now.ToBinary()),
                Description = categoryName.ToLower(),
            };

            db.Categories.Add(toInsert);
            Id = toInsert.Id;

            return db.SaveChanges() == 1;
        }

        public (int createdCategory, int createdEntry) Import(IList<ExportRow> rows)
        {
            int createdCategory = 0;
            foreach (var category in rows.Select(x => x.CategoryName.ToLower()).Distinct())
            {
                if (TryCreateCategory(category, out var _))
                    ++createdCategory;
            }

            using var db = ConnectDatabase();

            var categories = db.Categories.ToDictionary(x => x.Description, x => x);

            var bulk = rows.Select(row => new Spending
            {
                Date = row.Date,
                Description = row.Description,
                AddedOn= row.AddedOn,
                Ammount= row.Ammount,
                Category = categories[row.CategoryName],
                Id = CreateId(DateTime.UtcNow.ToBinary()),
            });

            db.Spendings.AddRange(bulk);
            int createdEntry =  db.SaveChanges();

            return (createdCategory, createdEntry);
        }
    }
}
