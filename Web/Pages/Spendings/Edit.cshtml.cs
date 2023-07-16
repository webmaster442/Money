using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Money.Web.Data;
using Money.Web.Data.Entity;

namespace Money.Web.Pages.Spendings
{
    [Authorize]
    internal class EditModel : PageModel
    {
        private readonly Money.Web.Data.ApplicationDbContext _context;

        public EditModel(Money.Web.Data.ApplicationDbContext context)
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

            var spending =  await _context.Spendings.FirstOrDefaultAsync(m => m.Id == id);
            if (spending == null)
            {
                return NotFound();
            }
            Spending = spending;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Spending).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpendingExists(Spending.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SpendingExists(int id)
        {
          return (_context.Spendings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
