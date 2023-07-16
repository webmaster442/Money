namespace Money.Web.Models
{
    internal class CreateSpendingViewModel
    {
        public List<CategorySelectorViewModel> CategorySelector { get; set; }
        public SpendingViewModel Spending { get; set; }

        public CreateSpendingViewModel()
        {
            Spending = new SpendingViewModel();
            CategorySelector = new List<CategorySelectorViewModel>();
        }
    }
}
