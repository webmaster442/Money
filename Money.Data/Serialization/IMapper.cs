using Money.Data.Entities;

namespace Money.Data.Serialization
{
    internal interface IMapper<T>
    {
        Spending ToDb(T toImport);
        T ToExport(Spending toExport);
    }
}
