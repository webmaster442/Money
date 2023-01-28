namespace Money.Data.Entities
{
    public sealed class Spending
    {
        public ulong Id { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }
        public double Ammount { get; set; }
        public DateTime AddedOn { get; set; }

        public Spending()
        {
            Description = string.Empty;
        }
    }
}
