using Money.Web.Services;

namespace Money.Web
{
    internal static class ServiceRegistrator
    {
        internal static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<VersionService>();
            services.AddScoped<CategoryServices>();
            services.AddScoped<SpendingServices>();
            services.AddScoped<ErrorCodeService>();
            services.AddScoped<ImportExportService>();
            services.AddScoped<StatisticsService>();
        }
    }
}
