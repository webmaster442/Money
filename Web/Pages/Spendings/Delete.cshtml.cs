using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Money.Web.Models;
using Money.Web.Services;

namespace Money.Web.Pages.Spendings
{
    [Authorize]
    internal class DeleteModel : PageModel
    {
        private readonly SpendingServices _spendingServices;

        public DeleteModel(SpendingServices spendingServices)
        {
            _spendingServices = spendingServices;
        }

        [BindProperty]
        public SpendingViewModel Spending { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.SpendingNotFound });
            }

            var spending = await _spendingServices.Get(HttpContext.User, id.Value);

            if (spending == null)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.SpendingNotFound });
            }
            else
            {
                Spending = spending;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.SpendingNotFound });
            }

            if (!await _spendingServices.Delete(HttpContext.User, Spending))
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.SpendingNotFound });
            }
            return RedirectToPage("./Index");
        }
    }
}
