using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Money.Web.Models;
using Money.Web.Services;

namespace Money.Web.Pages.Categories
{
    [Authorize]
    internal class CategorySpendingModel : PageModel
    {
        private readonly SpendingServices _spendingServices;

        public CategorySpendingModel(SpendingServices spendingServices)
        {
            _spendingServices = spendingServices;
        }

        public IList<ListSpendingViewModel> Spending { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.CategoryNotFound });
            }

            Spending = await _spendingServices.GetByCategory(HttpContext.User, id.Value);

            if (Spending == null 
                || Spending.Count < 1)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.CategoryNotFound });
            }

            return Page();
        }
    }
}
