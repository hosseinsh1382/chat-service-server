using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Business;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public AuthenticationService(ITokenGeneratorService tokenGeneratorService, AppDbContext dbContext, IMapper mapper)
    {
        _tokenGeneratorService = tokenGeneratorService;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<string> SignUpAsync(SignUpRequest signUpRequest)
    {
        var account = _mapper.Map<Account>(signUpRequest);
        await _dbContext.Accounts.AddAsync(account);
        await _dbContext.SaveChangesAsync();
        return await _tokenGeneratorService.GenerateTokenAsync(new GenerateTokenRequest
        {
            Id = account.Id,
            Username = account.Username
        });
    }
}