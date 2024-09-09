using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Business;

public class MessageService : IMessageService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public MessageService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<MessageResponse> AddMessageAsync(NewMessageRequest messageRequest)
    {
        var message = _mapper.Map<Message>(messageRequest);
        /*await _dbContext.Messages.AddAsync(message);
        await _dbContext.SaveChangesAsync();*/
        return _mapper.Map<MessageResponse>(message);
    }

    public async Task<string> GetClientConnectionId(long chatId)
    {
        var accountId = (await _dbContext.Chats.SingleOrDefaultAsync(c => c.ChatId == chatId) ??
                         throw new Exception("Account not found.")).AccountId;
        var connection = await _dbContext.AccountConnections.SingleOrDefaultAsync(x => x.AccountId == accountId);
        return connection.ConnectionId;
    }
}