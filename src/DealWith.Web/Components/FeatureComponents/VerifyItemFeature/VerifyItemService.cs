namespace DealWith.Web.Components.FeatureComponents.VerifyItemFeature;

public class VerifyItemService : IVerifyItemService
{
    public async Task<VerifyItemResult> VerifyItemAsync(VerifyItemInfo item)
    {
        await Task.CompletedTask;
        return new VerifyItemResult
        {
            ItemId = item.ItemId,
            IsValid = true
        };
    }
}
