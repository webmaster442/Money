using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Money.Web.Data;
using Money.Web.Data.Entity;

namespace Money.Web.Pages.Spendings
{
    [Authorize]
    internal class CreateModel : PageModel
    {
        private readonly Money.Web.Data.ApplicationDbContext _context;

        public CreateModel(Money.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Spending Spending { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Spendings == null || Spending == null)
            {
                return Page();
            }

            _context.Spendings.Add(Spending);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
