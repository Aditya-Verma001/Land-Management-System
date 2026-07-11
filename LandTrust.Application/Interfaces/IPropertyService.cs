using LandTrust.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.Interfaces;

public interface IPropertyService
{
    Task<string> TransferProperty(Guid sellerId, TransferRequestDto request);
    Task<CreatePropertyResponseDto> CreateProperty(CreatePropertyDto request);
    Task<List<PropertyHistoryDto>> GetPropertyHistory(Guid propertyId);

    Task<CurrentOwnerDto?> GetCurrentOwner(Guid propertyId);
    Task<List<PropertyHistoryDto>> GetActiveOwnerships();

    Task<List<PropertyHistoryDto>> GetInactiveOwnerships();

    Task<List<PropertyHistoryDto>> GetOwnershipHistory(DateTime from, DateTime to);

    Task<TransferRequestResponseDto> SubmitTransferRequest(
    SubmitTransferRequestDto request);

    Task<TransferRequestResponseDto> VerifyTransferRequest(
    Guid requestId,
    Guid officerId);

    Task<TransferRequestResponseDto> ApproveTransferRequest(
        Guid requestId,
        Guid officerId,
        string remarks);

    Task<TransferRequestResponseDto> CompleteTransferRequest(
        Guid requestId);

    Task<List<PendingTransferDto>> GetPendingTransfers();

    Task<OfficerDashboardDto> GetDashboardAsync();

    Task<PropertyDocumentResponseDto> UploadDocument(
    UploadPropertyDocumentDto request);

    Task<FileDownloadDto?> DownloadDocument(Guid documentId);

    Task<PagedResponse<PropertyListDto>> SearchProperties(PropertySearchDto request);
}
