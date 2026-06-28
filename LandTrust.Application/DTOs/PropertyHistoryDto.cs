using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.DTOs
{
    public class PropertyHistoryDto
    {
        public Guid OwnerUserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IsActive { get; set; }
    }
}
