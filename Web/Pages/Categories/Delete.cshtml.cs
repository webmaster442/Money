using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Money.Web.Models;
using Money.Web.Services;

namespace Money.Web.Pages.Categories
{
    [Authorize]
    internal class DeleteModel : PageModel
    {
        private readonly CategoryServices _categoryServices;

        public DeleteModel(CategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [BindProperty]
        public CategoryViewModel Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.CategoryNotFound });
            }

            var category = await _categoryServices.Get(HttpContext.User, id.Value);

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
            if (id == null)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.CategoryNotFound });
            }

            if (!await _categoryServices.Delete(HttpContext.User, Category))
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.CategoryNotFound });
            }
            return RedirectToPage("./Index");
        }
    }
}
