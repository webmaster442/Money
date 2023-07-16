namespace Money.Data.Entities;

internal sealed class Spending
{
    public ulong Id { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; }
    public double Ammount { get; set; }
    public DateTime AddedOn { get; set; }

    public Category Category { get; set; } = null!;

    public Spending()
    {
        Description = string.Empty;
    }
}
