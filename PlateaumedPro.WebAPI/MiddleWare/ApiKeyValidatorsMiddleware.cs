using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PlateaumedPro.Contracts;

namespace PlateaumedPro.WebAPI.MiddleWare
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "Authorization";
        public async Task OnActionExecutionAsync
               (ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue
                (APIKEYNAME, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key was not provided"
                };
                return;
            }

            var basicAuth = context.HttpContext.RequestServices.GetRequiredService<IBasicAuthService>();

            var apiKey = extractedApiKey.ToString().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();

            if (!basicAuth.CheckValidUserKey(apiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key is not valid"
                };
                return;
            }

            await next();
        }
    }
}