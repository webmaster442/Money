using System.Security.Claims;

using Microsoft.EntityFrameworkCore;

using Money.Web.Data;
using Money.Web.Data.Entity;

namespace Money.Web.Services
{
    internal sealed class ImportExportService : UserServiceBase
    {
        public ImportExportService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        private static string CreateCsvRow(Spending s)
        {
            return $"{s.Date};{s.Description};{s.Ammount};{s.AddedOn};{s.Category.Name}";
        }

        public async Task CreateExportFile(ClaimsPrincipal claims, HttpResponse httpResponse)
        {
            httpResponse.ContentType = "text/csv";
            httpResponse.StatusCode = 200;
            httpResponse.Headers["Content-Disposition"] = "attachment; filename=\"export.csv";

            var data = _applicationDbContext.Spendings
                .AsNoTracking()
                .Include(s => s.User)
                .Include(s => s.Category)
                .Where(s => s.User == GetUser(claims))
                .AsAsyncEnumerable();

            await foreach (var item in data) 
            {
                var row = CreateCsvRow(item);
                await httpResponse.WriteAsync(row);
            }
        }
    }
}
