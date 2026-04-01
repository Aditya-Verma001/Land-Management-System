using Microsoft.AspNetCore.Mvc;
using LandTrust.Application.DTOs;
using LandTrust.Application.Interfaces;

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
    public IActionResult CreateProperty([FromBody] CreatePropertyDto request)
    {
        try
        {
            var result = _propertyService.CreateProperty(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("transfer")]
    public IActionResult TransferProperty([FromBody] TransferRequestDto request)
    {
        try
        {
            _propertyService.TransferProperty(
                request.PropertyId,
                request.SellerId,
                request.BuyerId
            );

            return Ok("Property transferred successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}