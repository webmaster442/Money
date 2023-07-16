using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

using Money.Web.Data;

namespace Money.Web.Services
{
    internal abstract class UserServiceBase
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public UserServiceBase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IdentityUser GetUser(ClaimsPrincipal claims)
        {
            if (claims.Identity == null)
                throw new InvalidOperationException("Claims not set correctly");

            return _applicationDbContext.Users.Where(x => x.UserName == claims.Identity.Name).First();
        }

    }
}
