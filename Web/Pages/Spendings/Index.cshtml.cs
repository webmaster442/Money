using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Money.Web.Models;
using Money.Web.Services;

namespace Money.Web.Pages.Spendings
{
    [Authorize]

    internal class IndexModel : PageModel
    {
        private readonly SpendingServices _spendingService;

        public IndexModel(SpendingServices spendingService)
        {
            _spendingService = spendingService;
        }

        public IList<ListSpendingViewModel> Spending { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public async Task OnGetAsync(DateTime? start, DateTime? end)
        {
            EndDate = end ?? DateTime.Now.Date;
            StartDate = start ?? DateTime.Now.Subtract(TimeSpan.FromDays(31)).Date;

            Spending = await _spendingService.Get(HttpContext.User, StartDate, EndDate);
        }
    }
}
