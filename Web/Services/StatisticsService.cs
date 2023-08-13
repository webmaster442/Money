using System.Security.Claims;

using Microsoft.EntityFrameworkCore;

using Money.Web.Data;
using Money.Web.Models;

namespace Money.Web.Services
{
    internal class StatisticsService : UserServiceBase
    {
        public StatisticsService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<StatisticsViewModel> Get(ClaimsPrincipal claims, DateTime startDate, DateTime endDate)
        {
            if (claims.Identity == null)
                throw new InvalidOperationException("Claims not set correctly");

            var query = _applicationDbContext.Spendings
                .AsNoTracking()
                .Include(s => s.User)
                .Include(s => s.Category)
                .Where(s => s.User.UserName == claims.Identity.Name)
                .Where(s => s.Date >= startDate)
                .Where(s => s.Date <= endDate)
                .AsAsyncEnumerable();

            int count = 0;
            double sumSpending = 0;
            SpendingViewModel minimum = new() { Ammount = double.MaxValue };
            SpendingViewModel maximum = new() { Ammount = double.MinValue };
            Dictionary<DateOnly, double> spendingsPerDay = new();
            Dictionary<string, double> spendigsPerCategory = new();

            await foreach (var spending in query)
            {
                sumSpending += spending.Ammount;

                if (spending.Ammount < minimum.Ammount)
                    minimum = CreateViewModel(spending);

                if (spending.Ammount > maximum.Ammount)
                    maximum = CreateViewModel(spending);

                var spendingDay = spending.Date.ToDateOnly();

                if (spendingsPerDay.ContainsKey(spendingDay))
                    spendingsPerDay[spendingDay] += spending.Ammount;
                else
                    spendingsPerDay.Add(spendingDay, spending.Ammount);

                if (spendigsPerCategory.ContainsKey(spending.Category.Name))
                    spendigsPerCategory[spending.Category.Name] += spending.Ammount;
                else
                    spendigsPerCategory.Add(spending.Category.Name, spending.Ammount);

                ++count;
            }

            return new StatisticsViewModel
            {
                Maximum = maximum,
                SumSpending = sumSpending,
                Minimum = minimum,
                SpendigsPerDay = spendingsPerDay,
                SpendigsPerCategory = spendigsPerCategory,
                SpendingCount = count
            };
        }
    }
}
