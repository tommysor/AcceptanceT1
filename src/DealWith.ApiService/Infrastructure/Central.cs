using DealWith.ApiService.Features.VerifyItemFeature.Contracts;
using DealWith.ApiService.Features.VerifyItemFeature.Infrastructure;

namespace DealWith.ApiService.Infrastructure;

public sealed class Central : ICentral
{
    private readonly HttpClient _httpClient;

    public Central(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CentralVerifyResponse> Verify(CentralVerifyRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("central-verify", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<CentralVerifyResponse>(cancellationToken: cancellationToken);
        if (result is null)
        {
            throw new InvalidOperationException("The response from the central service was empty.");
        }
        return result;
    }
}
