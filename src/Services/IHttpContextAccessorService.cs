namespace SimpleSignalRChatApp.Services
{
    public interface IHttpContextAccessorService
    {
        Task<string> GetUserChatCookieValue();
        Task SetChatCookie(HttpResponse response);
    }
}
