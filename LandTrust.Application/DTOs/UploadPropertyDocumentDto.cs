using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LandTrust.Application.DTOs;

public class UploadPropertyDocumentDto
{
    [Required]
    public Guid PropertyId { get; set; }


    [Required]
    public IFormFile File { get; set; } = default!;
}