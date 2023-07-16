using Microsoft.AspNetCore.Identity;

namespace Money.Web.Data.Entity
{
    internal sealed class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IdentityUser User { get; set; } = null!;

        public Category()
        {
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}
