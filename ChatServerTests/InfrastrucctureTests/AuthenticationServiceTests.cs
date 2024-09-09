using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
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
        // Act
        var actual = await _authenticationService.SignUpAsync(signUpRequest);

        // Assert
        _dbContext.Accounts.Should().ContainEquivalentOf(account, options => options.Excluding(x => x.Id));
    }
}