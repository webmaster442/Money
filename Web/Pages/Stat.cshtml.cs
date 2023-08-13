using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Money.Web.Models;
using Money.Web.Services;

namespace Money.Web.Pages
{
    [Authorize]
    internal class StatModel : PageModel
    {
        private readonly StatisticsService _statisticsService;

        public StatModel(StatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public StatisticsViewModel Statistics { get; set; } = default!;

        public async Task OnGetAsync(DateTime? start, DateTime? end)
        {
            EndDate = end ?? DateTime.Now.Date;
            StartDate = start ?? DateTime.Now.Subtract(TimeSpan.FromDays(31)).Date;

            Statistics = await _statisticsService.Get(HttpContext.User, StartDate, EndDate);
        }
    }
}
