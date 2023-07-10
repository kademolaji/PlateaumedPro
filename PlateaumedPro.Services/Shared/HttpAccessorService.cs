using Microsoft.AspNetCore.Http;
using PlateaumedPro.Contracts;


namespace PlateaumedPro.Services
{
    public class HttpAccessorService : IHttpAccessorService
    {
        private readonly IHttpContextAccessor _httpAccessor;
        public HttpAccessorService(IHttpContextAccessor httpContextAccessor)
        {
            _httpAccessor = httpContextAccessor;
        }


        public String GetClientIP()
        {
            return _httpAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        public String GetHostAddress()
        {
            return _httpAccessor.HttpContext.Request.Host.Host.ToString();
        }
    }
}
