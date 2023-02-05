namespace Money.Tests
{
    internal class TestTb : DataAccessBase
    {
        public TestTb(IDatabaseFileLocator databaseLocator) : base(databaseLocator)
        {
        }

        public int CategoryCount
        {
            get
            {
                using var db = ConnectDatabase();
                return db.Categories.Count();
            }
        }

        public int SpendingCount
        {
            get
            {
                using var db = ConnectDatabase();
                return db.Spendings.Count();
            }
        }
    }
}
