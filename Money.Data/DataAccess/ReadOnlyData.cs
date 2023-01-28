using Money.Data.Serialization;

namespace Money.Data.DataAccess
{
    public class ReadOnlyData : IReadonlyData
    {
        private readonly IMapper<SerializableSpending> _mapper;

        public ReadOnlyData()
        {
            _mapper = new SerializableSpendingMapper();
        }

        private static MoneyContext ConnectDatabase()
        {
            return new MoneyContext();
        }

        public List<SerializableSpending> Export()
        {
            using (var db = ConnectDatabase())
            {
                return db.Spendings.Select(x => _mapper.ToExport(x)).ToList();
            }
        }
    }
}
