using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Money.Web.Models;
using Money.Web.Services;

namespace Money.Web.Pages.Spendings
{
    [Authorize]
    internal class CreateModel : PageModel
    {
        private readonly CategoryServices _categoryServices;
        private readonly SpendingServices _spendingService;

        public CreateModel(CategoryServices categoryServices, SpendingServices spendingService)
        {
            _categoryServices = categoryServices;
            _spendingService = spendingService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Spending = new SpendingViewModel
            {
                Date = DateTime.Now.Date,
            };

            CategorySelector = await _categoryServices.GetCategorySelector(HttpContext.User);

            return Page();
        }

        [BindProperty]
        public IList<CategorySelectorViewModel> CategorySelector { get; set; } = default!;

        [BindProperty]
        public SpendingViewModel Spending { get; set; } = default!;


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
