using Microsoft.AspNetCore.SignalR;
using SimpleSignalRChatApp.Models;
using SimpleSignalRChatApp.Services;

namespace SimpleSignalRChatApp.Hubs
{
    public class ChatHub: Hub
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IHubContext<AgentHub> _agentHub;
        private readonly IHttpContextAccessorService _httpContextAccessorService;

        public ChatHub(IChatRoomService chatRoomService, IHubContext<AgentHub> agentHub, IHttpContextAccessorService httpContextAccessorService)
        {
            _chatRoomService = chatRoomService;
            _agentHub = agentHub;
            _httpContextAccessorService = httpContextAccessorService;
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                // Authenticated agents don't need a room
                await base.OnConnectedAsync();
                return;
            }            

            var chatCookieValue=await _httpContextAccessorService.GetUserChatCookieValue(Context.GetHttpContext());
            if(chatCookieValue != null)
            {
                var roomId= await _chatRoomService.GetRoomIdByCookie(chatCookieValue);
                if (roomId!=Guid.Empty)
                {
                    var history = await _chatRoomService
                    .GetMessageHistory(roomId);

                    await Clients.Caller.SendAsync(
                        "ReceiveMessageesHistory", history);

                    await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());                    
                    await base.OnConnectedAsync();
                }
                else
                {
                    var newRoomId = await _chatRoomService.CreateRoom(chatCookieValue, Context.ConnectionId);
                    await Groups.AddToGroupAsync(Context.ConnectionId, newRoomId.ToString());
                    await Clients.Caller.SendAsync(
                        "ReceiveMessage",
                        "ShopAssist",
                        DateTimeOffset.UtcNow,
                        "Hello! How can i help you?");
                    await base.OnConnectedAsync();
                }
                return;
            }
            throw new Exception("cookie error");
                       
        }

        public async Task SendMessage(string name, string text)
        {
            var chatCookieValue = await _httpContextAccessorService.GetUserChatCookieValue(Context.GetHttpContext());
            var roomId = await _chatRoomService.GetRoomIdByCookie(chatCookieValue);

            var message = new ChatMessage
            {
                SenderName = name,
                Text = text,
                SentAt = DateTimeOffset.UtcNow
            };

            await _chatRoomService.AddMessage(roomId, message);

            // Broadcast to all clients
            await Clients.Group(roomId.ToString()).SendAsync(
                "ReceiveMessage",
                message.SenderName,
                message.SentAt,
                message.Text);

        }

        public async Task SetName(string visitorName)
        {
            var roomName = $"Chat with {visitorName} from the web";

            var chatCookieValue = await _httpContextAccessorService.GetUserChatCookieValue(Context.GetHttpContext());
            var roomId = await _chatRoomService.GetRoomIdByCookie(
                chatCookieValue);

            await _chatRoomService.SetRoomName(roomId, roomName);

            await _agentHub.Clients.All
                .SendAsync(
                    "ActiveRooms",
                    await _chatRoomService.GetAllRooms());
        }

       
        public async Task JoinRoom(Guid roomId)
        {
            if (roomId == Guid.Empty)
                throw new ArgumentException("Invalid room ID");

            await Groups.AddToGroupAsync(
                Context.ConnectionId, roomId.ToString());
        }

       
        public async Task LeaveRoom(Guid roomId)
        {
            if (roomId == Guid.Empty)
                throw new ArgumentException("Invalid room ID");

            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId, roomId.ToString());
        }
    }
}
