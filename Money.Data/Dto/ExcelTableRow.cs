namespace Money.Data.Dto
{
    public sealed class ExcelTableRow
    {
        public DateOnly Date { get; set; }
        public string Description { get; set; }
        public double Ammount { get; set; }
        public DateTime AddedOn { get; set; }
        public string CategoryName { get; set; }

        public ExcelTableRow()
        {
            Description = string.Empty;
            CategoryName = string.Empty;
        }
    }
}
