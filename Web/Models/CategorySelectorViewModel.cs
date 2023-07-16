namespace Money.Web.Models
{
    public class CategorySelectorViewModel
    {
        public CategorySelectorViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}
