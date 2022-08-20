namespace SimpleSignalRChatApp.Services
{
    public class HttpContextAccessorService : IHttpContextAccessorService
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //public HttpContextAccessorService(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}
        public Task<string> GetUserChatCookieValue(HttpContext httpContext)
        {
            var value = httpContext.Request.Cookies["olsc_cod"] ?? string.Empty;
            return Task.FromResult(value);
        }
        public Task SetChatCookie(HttpContext httpContext)
        {
            
            var olsc_cod = GetUserChatCookieValue(httpContext);
            if (olsc_cod == null)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(1);
                httpContext.Response.Cookies.Append("olsc_cod", Guid.NewGuid().ToString(), option);                
            }            
            return Task.CompletedTask;
        }
    }
}
