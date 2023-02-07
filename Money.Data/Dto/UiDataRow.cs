using Money.Data.Entities;

namespace Money.Data.Dto
{
    public sealed class UiDataRow
    {
        public DateOnly Date { get; set; }
        public string Description { get; set; }
        public double Ammount { get; set; }
        public string CategoryName { get; set; }

        public UiDataRow() 
        {
            Description= string.Empty;
            CategoryName = string.Empty;
        }

        internal UiDataRow(Spending spending)
        {
            Date = spending.Date;
            Description = spending.Description;
            Ammount = spending.Ammount;
            CategoryName = spending.Category.Description;
        }
    }
}
