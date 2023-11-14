using Microsoft.AspNetCore.SignalR;

namespace Telemedicina_TCC.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(String user,  String message, String channelId, String channelVal)
        {
            await Clients.All.SendAsync("ReciveMessage", user, message, channelId, channelVal).ConfigureAwait(false);
        }
    }
}
