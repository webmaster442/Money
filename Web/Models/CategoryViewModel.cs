namespace Money.Web.Models
{
    internal class CategoryViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long Id { get; set; }

        public CategoryViewModel() 
        {
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}
