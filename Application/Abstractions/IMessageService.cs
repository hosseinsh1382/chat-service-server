using Application.DTOs;
using Domain.Models;

namespace Application.Abstractions;

public interface IMessageService
{
    Task<MessageResponse> AddMessageAsync(NewMessageRequest messageRequest);

    Task<string> GetClientConnectionId(long chatId);
}