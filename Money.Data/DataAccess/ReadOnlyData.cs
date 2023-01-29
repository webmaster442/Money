using Money.Data.Dto;
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
            using var db = ConnectDatabase();
            return db
                .Spendings
                .Select(x => _mapper.ToExport(x))
                .ToList();
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
