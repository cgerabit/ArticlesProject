using System.Security.Claims;

namespace ProductsStoreApi.ExtensionMethods
{
    public static class HttpContextExtensionMethods
    {

        public static string? GetUserId(this HttpContext httpContext)
        {

            if(httpContext == null || httpContext.User == null)
            {
                return null;
            }

            return httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
