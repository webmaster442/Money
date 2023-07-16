using System.Security.Claims;

using Microsoft.EntityFrameworkCore;

using Money.Web.Data;
using Money.Web.Data.Entity;
using Money.Web.Models;

namespace Money.Web.Services
{
    internal sealed class CategoryServices : UserServiceBase
    {
        public CategoryServices(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<List<CategoryViewModel>> GetCategories(ClaimsPrincipal claims)
        {
            if (claims.Identity == null)
                throw new InvalidOperationException("Claims not set correctly");

            return await _applicationDbContext.Categories
                .AsNoTracking()
                .Include(c => c.User)
                .Where(c => c.User.UserName == claims.Identity.Name)
                .Select(c => new CategoryViewModel
                {
                    Name  = c.Name,
                    Description = c.Description,
                    Id = c.Id,
                })
                .ToListAsync();
        }

        public async Task<CategoryViewModel?> GetCategory(ClaimsPrincipal claims, long id)
        {
            if (claims.Identity == null)
                throw new InvalidOperationException("Claims not set correctly");

            return await _applicationDbContext.Categories
                .AsNoTracking()
                .Include(c => c.User)
                .Where(c => c.User.UserName == claims.Identity.Name)
                .Where(c => c.Id == id)
                .Select(c => new CategoryViewModel
                {
                    Name = c.Name,
                    Description = c.Description,
                    Id = c.Id,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Modify(ClaimsPrincipal claims, CategoryViewModel categoryViewModel)
        {
            if (claims.Identity == null)
                throw new InvalidOperationException("Claims not set correctly");

            var category = await _applicationDbContext.Categories
                .Include(c => c.User)
                .Where(c => c.User.UserName == claims.Identity.Name)
                .Where(c => c.Id == categoryViewModel.Id)
                .FirstAsync();

            category.Name = categoryViewModel.Name;
            category.Description = categoryViewModel.Description;

            return await _applicationDbContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> Create(ClaimsPrincipal claims, CategoryViewModel categoryViewModel)
        {
            var newCategory = new Category
            {
                Description = categoryViewModel.Description,
                Name = categoryViewModel.Name,
                User = GetUser(claims)
            };

            _applicationDbContext.Categories.Add(newCategory);
            return await _applicationDbContext.SaveChangesAsync() == 1;
        }
    } 
}