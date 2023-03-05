using Microsoft.AspNetCore.SignalR;
namespace SignalR_Chat_App.Hubs;

public class ChatHub:Hub
{
    public Task SendMessage(string user,string message)
    {
        return Clients.All.SendAsync("ReciveMessage",user, message);
    }
}
