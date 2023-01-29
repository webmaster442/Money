namespace Money.Data.Entities
{
    internal class Category
    {
        public ulong Id { get; set; }
        public string Description { get; set; }

        public Category()
        {
            Description = string.Empty;
        }
    }
}
