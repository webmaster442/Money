using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Money.Web.Services;

namespace Money.Web.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorDbModel : PageModel
{
    private readonly ErrorCodeService _errorCodeService;

    [BindProperty]
    public string Error { get; set; } = default!;

    public ErrorDbModel(ErrorCodeService errorCodeService)
    {
        _errorCodeService = errorCodeService;
    }

    public IActionResult OnGet(ErrorCode? ErrorCode)
    {
        var code = ErrorCode ?? Services.ErrorCode.Unknown;
        Error = _errorCodeService.GetErrorString(code);
        return Page();
    }
}

