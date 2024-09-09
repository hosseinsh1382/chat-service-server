using Application.Abstractions;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ChatServerTests.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpRequest signUpRequest)
    {
        var response = await _authenticationService.SignUpAsync(signUpRequest);
        return Ok(new { token = response });
    }
}