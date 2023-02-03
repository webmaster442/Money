using Microsoft.EntityFrameworkCore;

using Money.Data.Entities;

namespace Money.Data.DataAccess
{
    public abstract class DataAccessBase
    {
        private readonly IDatabaseFileLocator _databaseLocator;

        protected const int _ChunkSize = 50;

        protected DataAccessBase(IDatabaseFileLocator databaseLocator) 
        {
            _databaseLocator = databaseLocator;
        }

        internal MoneyContext ConnectDatabase()
        {
            return new MoneyContext(_databaseLocator);
        }

        internal static Task<Category?> GetCategory(MoneyContext context, string categoryName)
        {
            return context
                .Categories
                .Where(c => c.Description == categoryName.ToLower())
                .FirstOrDefaultAsync();
        }

        internal static Task<int> GetSpendingsCount(MoneyContext context)
        {
            return context.Spendings.CountAsync();
        }
    }
}
