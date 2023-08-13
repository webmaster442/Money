using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

using Money.Web.Data;
using Money.Web.Data.Entity;
using Money.Web.Models;

namespace Money.Web.Services
{
    internal abstract class UserServiceBase
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public UserServiceBase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        protected IdentityUser GetUser(ClaimsPrincipal claims)
        {
            if (claims.Identity == null)
                throw new InvalidOperationException("Claims not set correctly");

            return _applicationDbContext.Users.Where(x => x.UserName == claims.Identity.Name).First();
        }

        protected SpendingViewModel CreateViewModel(Spending s)
        {
            return new SpendingViewModel
            {
                Date = s.Date,
                Description = s.Description,
                Ammount = s.Ammount,
                Category = s.Category.Id,
                Id = s.Id,
            };
        }

    }
}
