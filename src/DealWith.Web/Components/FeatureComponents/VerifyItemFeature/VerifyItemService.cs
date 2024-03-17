namespace DealWith.Web.Components.FeatureComponents.VerifyItemFeature;

public class VerifyItemService : IVerifyItemService
{
    private readonly HttpClient _httpClient;

    public VerifyItemService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

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
