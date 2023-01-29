using System.Security.Cryptography;

using Money.Data.Dto;
using Money.Data.Entities;
using Money.Data.Serialization;

namespace Money.Data.DataAccess
{
    public sealed class WriteOnlyData : IWriteOnlyData
    {
        private readonly IMapper<SerializableSpending> _mapper;

        public WriteOnlyData()
        {
            _mapper = new SerializableSpendingMapper();
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
            var cat = db
                .Categories
                .Where(c => c.Description == category.ToLower())
                .FirstOrDefault();

            if (cat == null)
            {
                id = 0;
                return false;
            }

            var entry = db.Spendings.Add(new Entities.Spending
            {
                Id = CreateId(DateTime.UtcNow.ToBinary()),
                Date = date,
                Description = text,
                Ammount = ammount,
                AddedOn = DateTime.Now,
                Category = cat,
            });

            db.SaveChanges();

            id = entry.Entity.Id;
            return true;
        }

        public int Import(IEnumerable<SerializableSpending> toImport)
        {
            using MoneyContext db = ConnectDatabase();
            var toInsert = toImport.Select(spending =>
            {
                var entity = _mapper.ToDb(spending);
                entity.Id = CreateId(DateTime.UtcNow.ToBinary());
                return entity;
            });

            db.Spendings.AddRange(toInsert);

            return db.SaveChanges();
        }

        public bool TryCreateCategory(string categoryName, out ulong Id)
        {
            using MoneyContext db = ConnectDatabase();
            var exists = db
                .Categories
                .Where(c => c.Description == categoryName.ToLower())
                .Any();

            if (exists)
            {
                Id = 0;
                return false;
            }

            var toInsert = new Category
            {
                Id = CreateId(DateTime.Now.ToBinary()),
                Description = categoryName.ToLower(),
            };

            db.Categories.Add(toInsert);
            Id = toInsert.Id;

            return db.SaveChanges() == 1;
        }
    }
}
