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

        private static (Spending spending, string categoryName) ParseCsvRow(string line)
        {
            string[] tokens = line.Split(';', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 5)
                throw new InvalidDataException("Data is in bad format");

            try
            {
                Spending s = new Spending
                {
                    Date = DateTime.Parse(tokens[0]),
                    Description = tokens[1],
                    Ammount = double.Parse(tokens[2]),
                    AddedOn = DateTime.Parse(tokens[3]),
                };
                return (s, tokens[4]);
            }
            catch (Exception e)
            {
                throw new InvalidDataException("Data is in bad format", e);
            }
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

        public async Task<int> Import(ClaimsPrincipal claims, string[] lines)
        {
            var user = GetUser(claims);
            var categories = await _applicationDbContext.Categories.ToDictionaryAsync(x => x.Name, x => x);

            foreach (var line in lines) 
            {
                var parsed = ParseCsvRow(line);

                var toInsert = parsed.spending;

                toInsert.User = user;
                if (categories.ContainsKey(parsed.categoryName))
                {
                    toInsert.Category = categories[parsed.categoryName];
                }
                else
                {
                    var entry = _applicationDbContext.Categories.Add(new Category
                    {
                        Description = parsed.categoryName,
                        Name = parsed.categoryName,
                        User = user,
                    });
                    toInsert.Category = entry.Entity;
                }
                _applicationDbContext.Spendings.Add(toInsert);
            }

            return await _applicationDbContext.SaveChangesAsync();
        }

        public async Task CreateTemplate(HttpResponse httpResponse)
        {
            httpResponse.ContentType = "text/csv";
            httpResponse.StatusCode = 200;
            httpResponse.Headers["Content-Disposition"] = "attachment; filename=\"importTemplate.csv";

            var item = new Spending
            {
                Date = new DateTime(1999, 11, 12),
                Description = "Spending description",
                Ammount = 3000,
                AddedOn = DateTime.Now,
                Category = new Category
                {
                    Name = "Category Name"
                }
            };

            var row = CreateCsvRow(item);
            await httpResponse.WriteAsync(row);
        }
    }
}
