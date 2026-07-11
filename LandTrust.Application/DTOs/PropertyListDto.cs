namespace LandTrust.Application.DTOs;

public class PropertyListDto
{
    public Guid PropertyId { get; set; }

    public string State { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public string Village { get; set; } = string.Empty;

    public string SurveyNumber { get; set; } = string.Empty;

    public double Area { get; set; }
}