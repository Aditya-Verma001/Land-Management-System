using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandTrust.Domain.Entities;

public class PropertyDocument
{
    public Guid DocumentId { get; private set; }

    public Guid PropertyId { get; private set; }

    public string FileName { get; private set; } = string.Empty;

    public string FilePath { get; private set; } = string.Empty;

    public string ContentType { get; private set; } = string.Empty;

    public long FileSize { get; private set; }

    public DateTime UploadedAt { get; private set; }

    private PropertyDocument() { } // EF Core

    public PropertyDocument(
        Guid propertyId,
        string fileName,
        string filePath,
        string contentType,
        long fileSize)
    {
        DocumentId = Guid.NewGuid();
        PropertyId = propertyId;
        FileName = fileName;
        FilePath = filePath;
        ContentType = contentType;
        FileSize = fileSize;
        UploadedAt = DateTime.UtcNow;
    }
}