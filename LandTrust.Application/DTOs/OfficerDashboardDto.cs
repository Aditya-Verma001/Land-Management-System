using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandTrust.Application.DTOs;

public class OfficerDashboardDto
{
    public int PendingRequests { get; set; }

    public int VerifiedRequests { get; set; }

    public int ApprovedRequests { get; set; }

    public int RejectedRequests { get; set; }

    public int HighRiskRequests { get; set; }

    public int MediumRiskRequests { get; set; }

    public int LowRiskRequests { get; set; }
}
