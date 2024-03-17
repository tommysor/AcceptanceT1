namespace DealWith.ApiService.Features.VerifyItemFeature.Contracts;

public sealed record CentralVerifyResponse(string ItemId, bool IsValid, string? Message, Guid referenceId);
