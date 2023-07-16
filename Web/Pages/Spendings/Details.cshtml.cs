using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Money.Web.Data;
using Money.Web.Data.Entity;

namespace Money.Web.Pages.Spendings
{
    [Authorize]
    internal class DetailsModel : PageModel
    {
        private readonly Money.Web.Data.ApplicationDbContext _context;

        public DetailsModel(Money.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
