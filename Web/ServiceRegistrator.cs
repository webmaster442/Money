using Money.Web.Services;

namespace Money.Web
{
    internal class ServiceRegistrator
    {
        internal static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<VersionService>();
            services.AddScoped<CategoryServices>();
            services.AddScoped<SpendingService>();
        }
    }
}
