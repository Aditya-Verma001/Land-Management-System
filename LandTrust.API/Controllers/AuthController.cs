using LandTrust.Application.DTOs;
using LandTrust.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LandTrust.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto request)
    {
        var result = await _authenticationService.Register(request);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var result = await _authenticationService.Login(request);

        if (!result.Success)
            return Unauthorized(result);

        return Ok(result);
    }
}