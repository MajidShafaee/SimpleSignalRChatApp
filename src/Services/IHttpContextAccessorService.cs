namespace SimpleSignalRChatApp.Services
{
    public interface IHttpContextAccessorService
    {
        Task<string> GetUserChatCookieValue(HttpContext httpContext);
        Task SetChatCookie(HttpContext httpContext);
    }
}
