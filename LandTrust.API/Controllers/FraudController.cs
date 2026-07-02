using LandTrust.Application.DTOs;
using LandTrust.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LandTrust.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FraudController : ControllerBase
{
    private readonly IFraudDetectionService _fraudService;

    public FraudController(IFraudDetectionService fraudService)
    {
        _fraudService = fraudService;
    }

    [HttpPost("analyze")]
    public async Task<ActionResult<FraudCheckResultDto>> Analyze(
        FraudCheckRequestDto request)
    {
        var result = await _fraudService.AnalyzeAsync(request);

        return Ok(result);
    }
}