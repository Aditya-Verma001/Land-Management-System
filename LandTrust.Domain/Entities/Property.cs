using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Domain.Entities;

public class Property
{
    public Guid PropertyId { get; private set; }

    public string State { get; private set; }

    public string District { get; private set; }

    public string Village { get; private set; }

    public string SurveyNumber { get; private set; }

    public double Area { get; private set; }

    public double Latitude { get; private set; }

    public double Longitude { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Property(
        string state,
        string district,
        string village,
        string surveyNumber,
        double area,
        double latitude,
        double longitude)
    {
        PropertyId = Guid.NewGuid();
        State = state;
        District = district;
        Village = village;
        SurveyNumber = surveyNumber;
        Area = area;
        Latitude = latitude;
        Longitude = longitude;
        CreatedAt = DateTime.UtcNow;
    }
}
