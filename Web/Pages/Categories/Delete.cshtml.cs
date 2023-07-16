using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Money.Web.Data.Entity;
using Money.Web.Services;

namespace Money.Web.Pages.Categories
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
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Categories == null)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.CategoryNotFound });
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.CategoryNotFound });
            }
            else
            {
                Category = category;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);

            if (category != null)
            {
                Category = category;
                _context.Categories.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
