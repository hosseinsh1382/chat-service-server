using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Infrastructure.Business;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<NewMessageRequest, Message>();
        CreateMap<Message, MessageResponse>();
        CreateMap<SignUpRequest, Account>();
    }
}