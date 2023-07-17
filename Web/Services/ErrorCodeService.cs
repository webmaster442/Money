using System.Resources;

namespace Money.Web.Services
{
    public enum ErrorCode
    {
        CategoryExists,
        Unknown,
        CategoryNotFound,
        CategoryEditError,
        SpendingNotFound,
        SpendingEditError,
    }

    public class ErrorCodeService
    {
        public string GetErrorString(ErrorCode errorCode)
        {
            var msg = Properties.Resources.ResourceManager.GetString(errorCode.ToString());
            return msg ?? "Unknown";
        }
    }
}
