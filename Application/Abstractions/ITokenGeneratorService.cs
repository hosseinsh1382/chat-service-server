using Application.DTOs;

namespace Application.Abstractions;

public interface ITokenGeneratorService
{
    Task<string> GenerateTokenAsync(GenerateTokenRequest generateTokenRequest);
}