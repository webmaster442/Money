using System.ComponentModel.DataAnnotations;

namespace Money.Web.Models
{
    internal class SpendingViewModel
    {
        public int Id { get; set; }
        public int Category { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Ammount { get; set; }

        public SpendingViewModel()
        {
            Description = string.Empty;
        }
    }
}
