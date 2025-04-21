using Microsoft.AspNetCore.SignalR;

namespace M09T1PR2API_AlejandroMartin.HUBs
{
    public class ChatHub : Hub
    {
        public async Task EnviaMissatge(string user, string msg)
        {
            await Clients.All.SendAsync("GetMsg", user, msg);
        }
    }
}
