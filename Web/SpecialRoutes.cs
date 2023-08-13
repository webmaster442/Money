using Money.Web.Services;

namespace Money.Web
{
    public static class SpecialRoutes
    {
        public static void RegisterRoutes(WebApplication app)
        {
            app.Map("/downloadExport", HandleExport);
            app.Map("/downloadImportTemplate", HandleImportTemplate);
        }

        private static async Task HandleImportTemplate(HttpContext context, ImportExportService importExportService)
        {
            await importExportService.CreateTemplate(context.Response);
        }

        private static async Task HandleExport(HttpContext context, ImportExportService importExportService)
        {
            await importExportService.CreateExportFile(context.User, context.Response);
        }
    }
}
