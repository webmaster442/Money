using System.Security.Claims;

using Money.Web.Data;
using Money.Web.Models;

namespace Money.Web.Services
{
    internal class SpendingService : UserServiceBase
    {
        public SpendingService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
