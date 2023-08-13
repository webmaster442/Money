using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Money.Web.Services;

namespace Money.Web.Pages
{
    [Authorize]
    internal class ImportExportModel : PageModel
    {
        private readonly ImportExportService _importExportService;

        public ImportExportModel(ImportExportService importExportService)
        {
            _importExportService = importExportService;
        }

        [BindProperty]
        public IFormFile Upload { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                using (var reader = new StreamReader(Upload.OpenReadStream()))
                {
                    var data = await reader.ReadToEndAsync();
                    var lines = data.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                    await _importExportService.Import(HttpContext.User, lines);
                }
            }
            catch (Exception)
            {
                return RedirectToPage("/ErrorDb", new { ErrorCode = ErrorCode.ImportError });
            }

            return RedirectToPage("./Index");
        }
    }
}
