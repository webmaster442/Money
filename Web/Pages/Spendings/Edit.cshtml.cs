using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Money.Web.Models;
using Money.Web.Services;

namespace Money.Web.Pages.Spendings
{
    [Authorize]
    internal class EditModel : PageModel
    {
        private readonly CategoryServices _categoryServices;
        private readonly SpendingServices _spendingService;

        public EditModel(CategoryServices categoryServices, SpendingServices spendingService)
        {
            _categoryServices = categoryServices;
            _spendingService = spendingService;
        }

        [BindProperty]
        public IList<CategorySelectorViewModel> CategorySelector { get; set; } = default!;

        [BindProperty]
        public SpendingViewModel Spending { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.SpendingNotFound });
            }

            var spending = await _spendingService.Get(HttpContext.User, id.Value);

            if (spending == null)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.SpendingNotFound });
            }

            Spending = spending;
            CategorySelector = await _categoryServices.GetCategorySelector(HttpContext.User);
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

            try
            {
                if (!await _spendingService.Edit(HttpContext.User, Spending))
                {
                    return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.SpendingEditError });
                }
            }
            catch (DbUpdateException)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.SpendingEditError });
            }

            return RedirectToPage("./Index");
        }
    }
}
