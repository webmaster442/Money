namespace Money.Data.Entities
{
    internal sealed class Category
    {
        public ulong Id { get; set; }
        public string Description { get; set; }

        public Category()
        {
            Description = string.Empty;
        }
    }
}
