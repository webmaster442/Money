using System.Security.Cryptography;

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

        public ulong Insert(decimal ammount, string text, DateOnly date)
        {
            using (MoneyContext db = ConnectDatabase())
            {
                var entry = db.Spendings.Add(new Entities.Spending
                {
                    Id = CreateId(DateTime.UtcNow.ToBinary()),
                    Date = date,
                    Description = text,
                    Ammount = ammount,
                    AddedOn = DateTime.Now,
                }); ;

                db.SaveChanges();

                return entry.Entity.Id;
            }
        }

        public int Import(IEnumerable<SerializableSpending> toImport)
        {
            using (MoneyContext db = ConnectDatabase())
            {
                var toInsert = toImport.Select(spending =>
                {
                    var entity = _mapper.ToDb(spending);
                    entity.Id = CreateId(DateTime.UtcNow.ToBinary());
                    return entity;
                });

                db.Spendings.AddRange(toInsert);

                return db.SaveChanges();
            }
        }
    }
}
