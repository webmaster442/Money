using System.Security.Claims;

using Microsoft.EntityFrameworkCore;

using Money.Web.Data;
using Money.Web.Data.Entity;
using Money.Web.Models;

namespace Money.Web.Services
{
    internal class SpendingServices : UserServiceBase
    {
        public SpendingServices(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        private Category GetCategory(ClaimsPrincipal claims, int categoryId)
        {
            if (claims.Identity == null)
                throw new InvalidOperationException("Claims not set correctly");

            return _applicationDbContext.Categories
                .Include(c => c.User)
                .Where(c => c.User.UserName == claims.Identity.Name)
                .Where(c => c.Id == categoryId)
                .First();
        }

        public async Task<bool> Create(ClaimsPrincipal claims, SpendingViewModel viewModel)
        {
            var newSpending = new Spending
            {
                Description = viewModel.Description,
                Date = viewModel.Date,
                AddedOn = DateTime.Now,
                Ammount = viewModel.Ammount,
                User = GetUser(claims),
                Category = GetCategory(claims, viewModel.Category),
            };
            _applicationDbContext.Spendings.Add(newSpending);
            return await _applicationDbContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> Edit(ClaimsPrincipal claims, SpendingViewModel viewModel)
        {
            if (claims.Identity == null)
                throw new InvalidOperationException("Claims not set correctly");

            var spending = await _applicationDbContext.Spendings
                .Include(s => s.User)
                .Where(s => s.User.UserName == claims.Identity.Name)
                .Where(s => s.Id == viewModel.Id)
                .FirstAsync();

            spending.Ammount = viewModel.Ammount;
            spending.Date = viewModel.Date;
            spending.Description = viewModel.Description;
            spending.Category = GetCategory(claims, viewModel.Category);
            spending.AddedOn = DateTime.Now;

            return await _applicationDbContext.SaveChangesAsync() == 1;
        }

        public async Task<List<ListSpendingViewModel>> Get(ClaimsPrincipal claims, DateTime startDate, DateTime endDate)
        {
            if (claims.Identity == null)
                throw new InvalidOperationException("Claims not set correctly");

            return await _applicationDbContext.Spendings
                .Include(s => s.User)
                .Include(s => s.Category)
                .Where(s => s.User.UserName == claims.Identity.Name)
                .Where(s => s.Date >= startDate)
                .Where(s => s.Date <= endDate)
                .Select(s => new ListSpendingViewModel
                {
                    AddedOn = s.AddedOn,
                    Date = s.Date,
                    Ammount = s.Ammount,
                    Category = s.Category.Name,
                    Id = s.Id,
                    Description = s.Description,
                })
                .ToListAsync();
        }

        internal async Task<SpendingViewModel?> Get(ClaimsPrincipal claims, int id)
        {
            if (claims.Identity == null)
                throw new InvalidOperationException("Claims not set correctly");

            return await _applicationDbContext.Spendings
                .Include(s => s.User)
                .Include(s => s.Category)
                .Where(s => s.User.UserName == claims.Identity.Name)
                .Where(s => s.Id == id)
                .Select(s => new SpendingViewModel
                {
                    Date = s.Date,
                    Description = s.Description,
                    Ammount = s.Ammount,
                    Category = s.Category.Id,
                    Id = s.Id,
                })
                .FirstOrDefaultAsync();
        }
    }
}
