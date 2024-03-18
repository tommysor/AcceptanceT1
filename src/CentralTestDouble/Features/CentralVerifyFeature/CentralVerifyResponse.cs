namespace TestDoubles.CentralTestDouble.Features.CentralVerifyFeature;

public record CentralVerifyResponse(string ItemId, bool IsValid, string? Message, Guid ReferenceId);
