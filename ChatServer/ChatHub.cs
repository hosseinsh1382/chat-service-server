using Application.Abstractions;
using Application.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer;

public class ChatHub : Hub
{
    private readonly IMessageService _messageService;

    public ChatHub(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task SendMessage(NewMessageRequest message)
    {
        Console.WriteLine(message.Text);
        var messageResponse = await _messageService.AddMessageAsync(message);
        //var connection = await _messageService.GetClientConnectionId(message.ChatId);
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}