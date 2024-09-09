using Application.DTOs;

namespace Application.Abstractions;

public interface IAuthenticationService
{
    Task<string> SignUpAsync(SignUpRequest signUpRequest);
    Task<string> LoginAsync(LoginRequest loginRequest);
}