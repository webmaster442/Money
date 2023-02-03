namespace Money.Data.DataAccess
{
    public abstract class DataAccessBase
    {
        private readonly IDatabaseFileLocator _databaseLocator;

        protected DataAccessBase(IDatabaseFileLocator databaseLocator) 
        {
            _databaseLocator = databaseLocator;
        }

        internal MoneyContext ConnectDatabase()
        {
            return new MoneyContext(_databaseLocator);
        }
    }
}
