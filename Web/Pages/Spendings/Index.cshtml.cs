using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Money.Web.Data.Entity;

namespace Money.Web.Pages.Spendings
{
    [Authorize]
    internal class IndexModel : PageModel
    {
        private readonly Money.Web.Data.ApplicationDbContext _context;

        public IndexModel(Money.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Spending> Spending { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Spendings != null)
            {
                Spending = await _context.Spendings.ToListAsync();
            }
        }
    }
}
