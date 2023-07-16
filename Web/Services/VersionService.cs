namespace Money.Web.Services
{
    internal sealed class VersionService
    {
        public string GetVersion()
        {
            return typeof(VersionService)
                .Assembly
                .GetName()
                ?.Version
                ?.ToString() ?? "unknown";
        }
    }
}
