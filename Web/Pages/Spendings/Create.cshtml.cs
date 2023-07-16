using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Money.Web.Data.Entity;
using Money.Web.Models;
using Money.Web.Services;

namespace Money.Web.Pages.Spendings
{
    [Authorize]
    internal class CreateModel : PageModel
    {
        private readonly SpendingService _spendingService;

        public CreateModel(SpendingService spendingService)
        {
            _spendingService = spendingService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Spending = _spendingService.NewSpending(HttpContext.User);
            return Page();
        }

        [BindProperty]
        public SpendingViewModel Spending { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Spending == null)
            {
                return Page();
            }

            try
            {
                await _spendingService.Create(HttpContext.User, Spending);
            }
            catch (DbUpdateException)
            {
                return RedirectToPage("/ErrorDb");
            }

            return RedirectToPage("./Index");
        }
    }
}
