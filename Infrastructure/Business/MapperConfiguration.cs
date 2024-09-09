using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Infrastructure.Business;

public class MapperConfiguration : Profile
{
    public MapperConfiguration()
    {
        CreateMap<NewMessageRequest, Message>();
        CreateMap<Message, MessageResponse>();
    }
}