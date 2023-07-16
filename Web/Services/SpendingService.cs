﻿using System.Security.Claims;

using Microsoft.EntityFrameworkCore;

using Money.Web.Data;
using Money.Web.Data.Entity;
using Money.Web.Models;

namespace Money.Web.Services
{
    internal class SpendingService : UserServiceBase
    {
        public SpendingService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public CreateSpendingViewModel NewSpending(ClaimsPrincipal claims)
        {
            if (claims.Identity == null)
                throw new InvalidOperationException("Claims not set correctly");

            var categories = _applicationDbContext.Categories
                .Include(c => c.User)
                .Where(c => c.User.UserName == claims.Identity.Name)
                .Select(c => new CategorySelectorViewModel(c.Id, c.Name))
                .ToList();

            return new CreateSpendingViewModel
            {
                CategorySelector = categories,
                Spending = new SpendingViewModel
                {
                    Date = DateTime.Now,
                }
            };
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
    }
}
