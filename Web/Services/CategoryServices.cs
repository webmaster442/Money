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

        public async Task<bool> Create(ClaimsPrincipal claims, CategoryViewModel viewModel)
        {
            var newCategory = new Category
            {
                Description = viewModel.Description,
                Name = viewModel.Name,
                User = GetUser(claims)
            };

            _applicationDbContext.Categories.Add(newCategory);
            return await _applicationDbContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> Edit(ClaimsPrincipal claims, CategoryViewModel viewModel)
        {
            if (claims.Identity == null)
                throw new InvalidOperationException("Claims not set correctly");

            var category = await _applicationDbContext.Categories
                .Include(c => c.User)
                .Where(c => c.User.UserName == claims.Identity.Name)
                .Where(c => c.Id == viewModel.Id)
                .FirstAsync();

            category.Name = viewModel.Name;
            category.Description = viewModel.Description;

            return await _applicationDbContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> Delete(ClaimsPrincipal claims, CategoryViewModel viewModel)
        {
            if (claims.Identity == null)
                throw new InvalidOperationException("Claims not set correctly");

            var category = await _applicationDbContext.Categories.FindAsync(viewModel.Id);

            if (category != null)
            {
                _applicationDbContext.Categories.Remove(category);
                return await _applicationDbContext.SaveChangesAsync() == 1;
            }

            return false;
        }

        public async Task<List<CategoryViewModel>> Get(ClaimsPrincipal claims)
        {
            if (claims.Identity == null)
                throw new InvalidOperationException("Claims not set correctly");

            return await _applicationDbContext.Categories
                .AsNoTracking()
                .Include(c => c.User)
                .Where(c => c.User.UserName == claims.Identity.Name)
                .Select(c => new CategoryViewModel
                {
                    Name = c.Name,
                    Description = c.Description,
                    Id = c.Id,
                })
                .ToListAsync();
        }

        public async Task<CategoryViewModel?> Get(ClaimsPrincipal claims, long id)
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
    }
}