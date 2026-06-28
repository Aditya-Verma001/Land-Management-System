using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace LandTrust.Application.DTOs;

public class CreatePropertyDto
{
    [Required]
    public string State { get; set; } = string.Empty;

    [Required]
    public string District { get; set; } = string.Empty;

    [Required]
    public string Village { get; set; } = string.Empty;

    [Required]
    public string SurveyNumber { get; set; } = string.Empty;

    [Range(1, double.MaxValue)]
    public double Area { get; set; }

    [Range(-90, 90)]
    public double Latitude { get; set; }

    [Range(-180, 180)]
    public double Longitude { get; set; }

    [Required]
    public Guid OwnerId { get; set; }
}