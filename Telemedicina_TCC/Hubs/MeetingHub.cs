using Microsoft.AspNetCore.SignalR;
using Telemedicina_TCC.Areas.Identity.Data;

namespace Telemedicina_TCC.Hubs
{
    public class MeetingHub : Hub
    {
        public async Task JoimRoom (string roomId, string userId)
        {
            ApplicationUser.list.Add(Context.ConnectionId, userId);
            await Groups.AddToGroupAsync (Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("user-connected", userId);
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Clients.All.SendAsync("user-disconnected", ApplicationUser.list[Context.ConnectionId]);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
