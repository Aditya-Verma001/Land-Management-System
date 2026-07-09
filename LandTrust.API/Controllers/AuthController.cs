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
}