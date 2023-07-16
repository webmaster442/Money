using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Money.Web.Models;
using Money.Web.Services;

namespace Money.Web.Pages.Categories
{
    [Authorize]
    internal class CreateModel : PageModel
    {
        private readonly CategoryServices _categoryServices;

        public CreateModel(CategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CategoryViewModel Category { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Category == null)
            {
                return Page();
            }

            try
            {
                await _categoryServices.Create(HttpContext.User, Category);
            }
            catch (DbUpdateException)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.CategoryExists });
            }

            return RedirectToPage("./Index");
        }
    }
}
