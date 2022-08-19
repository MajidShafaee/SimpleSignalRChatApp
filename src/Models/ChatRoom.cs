namespace SimpleSignalRChatApp.Models
{
    public class ChatRoom
    {
        public ChatRoom()
        {
            ConnectionIds = new List<string>();
        }
        public string OwnerCookieId { get; set; }
        public List<string> ConnectionIds { get; set; }

        public string Name { get; set; }
    }
}
