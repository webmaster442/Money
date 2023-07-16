using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Money.Web.Data.Entity;

namespace Money.Web.Pages.Spendings
{
    [Authorize]
    internal class DeleteModel : PageModel
    {
        private readonly Money.Web.Data.ApplicationDbContext _context;

        public DeleteModel(Money.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Spending Spending { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Spendings == null)
            {
                return NotFound();
            }

            var spending = await _context.Spendings.FirstOrDefaultAsync(m => m.Id == id);

            if (spending == null)
            {
                return NotFound();
            }
            else
            {
                Spending = spending;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Spendings == null)
            {
                return NotFound();
            }
            var spending = await _context.Spendings.FindAsync(id);

            if (spending != null)
            {
                Spending = spending;
                _context.Spendings.Remove(Spending);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
