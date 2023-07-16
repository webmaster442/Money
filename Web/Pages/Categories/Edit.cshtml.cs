using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Money.Web.Models;
using Money.Web.Services;

namespace Money.Web.Pages.Categories
{
    [Authorize]
    internal class EditModel : PageModel
    {
        private readonly CategoryServices _categoryServices;

        public EditModel(CategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [BindProperty]
        public CategoryViewModel Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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
            Category = category;
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
                if (!await _categoryServices.Edit(HttpContext.User, Category))
                {
                    return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.CategoryEditError });
                }
            }
            catch (DbUpdateException)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.CategoryEditError });
            }

            return RedirectToPage("./Index");
        }
    }
}
