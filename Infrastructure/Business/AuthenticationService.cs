using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
        account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
        await _dbContext.Accounts.AddAsync(account);
        await _dbContext.SaveChangesAsync();
        return await _tokenGeneratorService.GenerateTokenAsync(new GenerateTokenRequest
        {
            Id = account.Id,
            Username = account.Username
        });
    }

    public async Task<string> LoginAsync(LoginRequest loginRequest)
    {
        var account = await _dbContext.Accounts.SingleOrDefaultAsync(x => x.Username == loginRequest.Username)??throw new AccountNotFoundException();

        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, account.Password))
        {
            throw new InvalidPasswordException();
        }

        var generateTokenRequest = new GenerateTokenRequest
        {
            Id = account.Id,
            Username = account.Username
        };
        return await _tokenGeneratorService.GenerateTokenAsync(generateTokenRequest);
    }
}