using LandTrust.Application.Common;
using LandTrust.Application.DTOs;
using LandTrust.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LandTrust.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyDto request)
    {
        var result = await _propertyService.CreateProperty(request);
        return Ok(ApiResponse<object>.SuccessResponse(result, "Created"));
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> TransferProperty([FromBody] TransferRequestDto request)
    {
        var result = await _propertyService.TransferProperty(request);
        return Ok(result);
    }

    [HttpGet("{propertyId}/history")]
    public async Task<IActionResult> GetHistory(Guid propertyId)
    {
        var result = await _propertyService.GetPropertyHistory(propertyId);
        return Ok(result);
    }

    [HttpGet("{propertyId}/current-owner")]
    public async Task<IActionResult> GetCurrentOwner(Guid propertyId)
    {
        var owner = await _propertyService.GetCurrentOwner(propertyId);

        if (owner == null)
            return NotFound("Current owner not found.");

        return Ok(owner);
    }

    [HttpGet("history/active")]
    public async Task<IActionResult> ActiveOwnerships()
    {
        return Ok(await _propertyService.GetActiveOwnerships());
    }

    [HttpGet("history/inactive")]
    public async Task<IActionResult> InactiveOwnerships()
    {
        return Ok(await _propertyService.GetInactiveOwnerships());
    }

    [HttpGet("history/date-range")]
    public async Task<IActionResult> OwnershipHistory(
    DateTime from,
    DateTime to)
    {
        return Ok(await _propertyService
            .GetOwnershipHistory(from, to));
    }
}