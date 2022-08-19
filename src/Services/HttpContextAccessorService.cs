namespace SimpleSignalRChatApp.Services
{
    public class HttpContextAccessorService : IHttpContextAccessorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpContextAccessorService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<string> GetUserChatCookieValue()
        {
            var value = _httpContextAccessor.HttpContext.Request.Cookies["olsc_cod"] ?? string.Empty;
            return Task.FromResult(value);
        }
        public Task SetChatCookie(HttpResponse response)
        {
            
            var olsc_cod = GetUserChatCookieValue();
            if (olsc_cod == null)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(1);
                response.Cookies.Append("olsc_cod", Guid.NewGuid().ToString(), option);
            }            
            return Task.CompletedTask;
        }
    }
}
