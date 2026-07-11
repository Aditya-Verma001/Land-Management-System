using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandTrust.Application.DTOs;

public class PropertyDocumentResponseDto
{
    public bool Success { get; set; }

    public Guid DocumentId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;
}