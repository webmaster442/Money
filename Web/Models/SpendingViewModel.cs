namespace Money.Web.Models
{
    public class SpendingViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
        public DateTime AddedOn { get; set; }
        public string Description { get; set; }
        public double Ammount { get; set; }

        public List<CategorySelectorViewModel> Categories { get; set; }

        public SpendingViewModel()
        {
            Description = string.Empty;
            Categories = new List<CategorySelectorViewModel>();
        }
    }
}
