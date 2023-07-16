using System.ComponentModel.DataAnnotations;

namespace Money.Web.Models
{
    public class ListSpendingViewModel
    {
        public int Id { get; init; }

        [DataType(DataType.Date)]
        public DateTime Date { get; init; }
        public string Category { get; init; }
        public string Description { get; init; }
        public double Ammount { get; init; }
        public DateTime AddedOn { get; init; }

        public ListSpendingViewModel()
        {
            Category = string.Empty;
            Description = string.Empty;
        }

    }
}
