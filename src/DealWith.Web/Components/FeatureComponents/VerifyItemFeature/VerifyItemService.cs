namespace DealWith.Web.Components.FeatureComponents.VerifyItemFeature;

public class VerifyItemService : IVerifyItemService
{
    private readonly HttpClient _httpClient;

    public VerifyItemService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<VerifyItemResult?> VerifyItemAsync(VerifyItemInfo item)
    {
        var request = new
        {
            ItemId = item.ItemId
        };
        var response = await _httpClient.PostAsJsonAsync("verify-item", request);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<VerifyItemResult>();
        return result;
    }
}
