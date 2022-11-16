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
    public async Task NewMessage(String username, string message) {
        _logger.LogInformation($"Message Received ({username}) - {message}");
        await Clients.All.SendAsync("messageReceived", username, message);
    }
}
}