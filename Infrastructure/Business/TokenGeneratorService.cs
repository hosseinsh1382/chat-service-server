using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstractions;
using Application.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Business;

public class TokenGeneratorService : ITokenGeneratorService
{
    private readonly IConfiguration _configuration;

    public TokenGeneratorService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<string> GenerateTokenAsync(GenerateTokenRequest generateTokenRequest)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, generateTokenRequest.Username),
            new("id", generateTokenRequest.Id.ToString())
        };

        var jwt = new JwtSecurityToken(
            claims: claims,
            signingCredentials:
            new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        );
        return Task.Run(() => new JwtSecurityTokenHandler().WriteToken(jwt));
    }
}