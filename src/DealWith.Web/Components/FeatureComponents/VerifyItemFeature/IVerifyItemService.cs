namespace DealWith.Web.Components.FeatureComponents.VerifyItemFeature;

public interface IVerifyItemService
{
    Task<VerifyItemResult> VerifyItemAsync(VerifyItemInfo item);
}
