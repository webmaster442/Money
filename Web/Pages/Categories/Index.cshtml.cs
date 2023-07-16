using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Money.Web.Models;
using Money.Web.Services;

namespace Money.Web.Pages.Categories
{
    [Authorize]
    internal class IndexModel : PageModel
    {
        private readonly CategoryServices _categoryServices;

        public IndexModel(CategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public IList<CategoryViewModel> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Category = await _categoryServices.GetCategories(HttpContext.User);
        }
    }
}
