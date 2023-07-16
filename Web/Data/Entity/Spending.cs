using Microsoft.AspNetCore.Identity;

namespace Money.Web.Data.Entity
{
    internal sealed class Spending
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime AddedOn { get; set; }
        public string Description { get; set; }
        public double Ammount { get; set; }
        public Category Category { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;

        public Spending()
        {
            Description = string.Empty;
        }
    }
}
