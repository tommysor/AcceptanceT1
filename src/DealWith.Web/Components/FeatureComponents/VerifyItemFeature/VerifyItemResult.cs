namespace DealWith.Web.Components.FeatureComponents.VerifyItemFeature;

public class VerifyItemResult
{
    public required string ItemId { get; set; }
    public bool IsValid { get; set; }
    public string? Message { get; set; }
}
