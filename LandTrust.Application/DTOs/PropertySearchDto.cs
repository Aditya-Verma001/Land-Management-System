namespace LandTrust.Application.DTOs;

public class PropertySearchDto
{
    public string? State { get; set; }

    public string? District { get; set; }

    public string? Village { get; set; }

    public string? SurveyNumber { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}