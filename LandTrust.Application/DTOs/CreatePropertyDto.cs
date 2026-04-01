using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.DTOs;

public class CreatePropertyDto
{
    public string State { get; set; }

    public string District { get; set; }

    public string Village { get; set; }

    public string SurveyNumber { get; set; }

    public double Area { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public Guid OwnerId { get; set; }
}