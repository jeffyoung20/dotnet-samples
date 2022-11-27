using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace API.Hubs
{
    public class ChatHub : Hub
    {
        ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public async Task NewMessage(String username, string message)
        {
            _logger.LogInformation($"Message Received ({username}) - {message}");
            await Clients.All.SendAsync("messageReceived", username, message);
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation("OnConnect event");

            string connectionId = Context.ConnectionId;
            _logger.LogInformation($"ConnectionID:  {connectionId}");

            // string? connectionName = Context.User.Identity.Name;
            // _logger.LogInformation($"User Name:  {connectionName}");

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation("OnDisConnect event");
            return base.OnDisconnectedAsync(exception);
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}