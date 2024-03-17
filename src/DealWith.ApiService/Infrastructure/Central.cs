using DealWith.ApiService.Features.VerifyItemFeature.Contracts;
using DealWith.ApiService.Features.VerifyItemFeature.Infrastructure;

namespace DealWith.ApiService.Infrastructure;

public sealed class Central : ICentral
{
    private readonly Random _random;

    public Central()
    {
        _random = new Random();
    }

    public Task<CentralVerifyResponse> Verify(CentralVerifyRequest request, CancellationToken cancellationToken)
    {
        var x = _random.Next(0, 2);
        var isValid = x == 1;
        var response = new CentralVerifyResponse(request.ItemId, isValid, "", Guid.NewGuid());
        return Task.FromResult(response);
    }
}
