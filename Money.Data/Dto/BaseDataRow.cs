namespace Money.Data.Dto;

public interface IDataRowBase
{
    string Description { get; init; }
    double Ammount { get; init; }
    string CategoryName { get; init; }
}
