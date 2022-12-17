using Microsoft.AspNetCore.SignalR;
using Order.Middleware;
using Order.Service;

namespace TakeFood.UserOrder.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    public readonly static ConnectionMapping<string> _connections =
             new ConnectionMapping<string>();
    public IJwtService JwtService { get; set; }

    public NotificationHub(IJwtService jwtService)
    {
        JwtService = jwtService;
    }

    public void SendChatMessage(string who, string message)
    {
        string name = Context.User.Identity.Name;

        foreach (var connectionId in _connections.GetConnections(who))
        {
            Clients.Client(connectionId).SendAsync(name + ": " + message);
        }
    }

    public override Task OnConnectedAsync()
    {
        var httpContext = this.Context.GetHttpContext();
        var token = httpContext.Request.Query["access_token"].ToString();
        if (token == null)
        {
            return null;
        }
        else
        {
            try
            {
                JwtService.ValidSecurityToken(token);
                string name = JwtService.GetId(token);
                _connections.Add(name, Context.ConnectionId);
                return base.OnConnectedAsync();
            }
            catch (Exception err)
            {
                return null;
            }
        }
    }

    public override Task OnDisconnectedAsync(Exception err)
    {
        string name = Context.User.Identity.Name;

        _connections.Remove(name, Context.ConnectionId);

        return base.OnDisconnectedAsync(err);
    }
}
