using SimpleSignalRChatApp.Models;

namespace SimpleSignalRChatApp.Services
{
    public interface IChatRoomService
    {
        Task<Guid> CreateRoom(string cookieId,string connectionId);        

        Task<Guid> GetRoomIdByCookie(string cookieId);        

        Task SetRoomName(Guid roomId, string name);

        Task AddMessage(Guid roomId, ChatMessage message);

        Task<IEnumerable<ChatMessage>> GetMessageHistory(Guid roomId);

        Task<IReadOnlyDictionary<Guid, ChatRoom>> GetAllRooms();
    }
}
