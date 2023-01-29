using Money.Data.Dto;
using Money.Data.Entities;

namespace Money.Data.Serialization
{
    internal sealed class SerializableSpendingMapper : IMapper<SerializableSpending>
    {
        public Spending ToDb(SerializableSpending toImport)
        {
            return new Spending
            {
                Date = toImport.Date,
                Description = toImport.Description,
                AddedOn = toImport.AddedOn,
                Ammount = toImport.Ammount,
            };
        }

        public SerializableSpending ToExport(Spending toExport)
        {
            return new SerializableSpending
            {
                Date = toExport.Date,
                Description = toExport.Description,
                AddedOn = toExport.AddedOn,
                Ammount = toExport.Ammount,
            };
        }
    }
}
