using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using FluentAssertions;
using Infrastructure.Business;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NSubstitute;


namespace ChatServerTests.InfrastrucctureTests;

public class AuthenticationServiceTests
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly IMapper _mapper;
    private readonly AppDbContext _dbContext;

    public AuthenticationServiceTests()
    {
        _tokenGeneratorService = Substitute.For<ITokenGeneratorService>();
        var option = new DbContextOptionsBuilder().UseInMemoryDatabase("test-database").Options;
        _dbContext = new AppDbContext(option);
        _mapper = new MapperConfiguration(x => x.AddProfile<AutoMapperConfiguration>()).CreateMapper();
        _authenticationService = new AuthenticationService(_tokenGeneratorService, _dbContext, _mapper);
    }

    [Fact]
    public async Task SignUpAsync_WhenEver_ReturnsString()
    {
        // Arrange
        var signUpRequest = new SignUpRequest
        {
            Email = "email@test.com",
            Firstname = "name",
            Lastname = "lastname",
            Password = "password",
            Username = "username"
        };
        var account = new Account
        {
            Email = "email@test.com",
            Firstname = "name",
            Lastname = "lastname",
            Password = "password",
            Username = "username"
        };
        _tokenGeneratorService.GenerateTokenAsync(Arg.Any<GenerateTokenRequest>()).Returns("token");
        // Act
        var actual = await _authenticationService.SignUpAsync(signUpRequest);

        // Assert
        _dbContext.Accounts.Should()
            .ContainEquivalentOf(account, options => options.Excluding(x => x.Id).Excluding(x => x.Password));
        actual.Should().Be("token");

        _dbContext.Accounts.RemoveRange(_dbContext.Accounts);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task LoginAsync_WhenCredentialsAreCorrect_ReturnsString()
    {
        // Arrange
        var account = new Account
        {
            Email = "email@test.com",
            Firstname = "name",
            Lastname = "lastname",
            Password = BCrypt.Net.BCrypt.HashPassword("password"),
            Username = "username",
        };
        var loginRequest = new LoginRequest
        {
            Username = "username",
            Password = "password"
        };
        _tokenGeneratorService.GenerateTokenAsync(Arg.Any<GenerateTokenRequest>()).Returns("token");
        await _dbContext.Accounts.AddAsync(account);
        await _dbContext.SaveChangesAsync();

        // Act
        var actual = await _authenticationService.LoginAsync(loginRequest);

        // Assert
        actual.Should().Be("token");

        _dbContext.Accounts.Remove(account);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task LoginAsync_WhenPasswordIsNotCorrect_ThrowsInvalidPasswordException()
    {
        // Arrange
        var account = new Account
        {
            Email = "email@test.com",
            Firstname = "name",
            Lastname = "lastname",
            Password = BCrypt.Net.BCrypt.HashPassword("password"),
            Username = "username",
        };
        var loginRequest = new LoginRequest
        {
            Username = "username",
            Password = "pass"
        };
        await _dbContext.Accounts.AddAsync(account);
        await _dbContext.SaveChangesAsync();

        // Act
        var action = async () => await _authenticationService.LoginAsync(loginRequest);

        // Assert
        await action.Should().ThrowAsync<InvalidPasswordException>();
        
        _dbContext.Accounts.Remove(account);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task LoginAsync_WhenUsernameIsNotCorrect_ThrowsInvalidPasswordException()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Username = "username",
            Password = "pass"
        };

        // Act
        var action = async () => await _authenticationService.LoginAsync(loginRequest);

        // Assert
        await action.Should().ThrowAsync<AccountNotFoundException>();
    }
}