using DealWith.ApiService.Features.VerifyItemFeature.Contracts;
using DealWith.ApiService.Features.VerifyItemFeature.Infrastructure;

namespace DealWith.ApiService.Infrastructure;

public sealed class Storage : IStorage
{
    private static readonly List<VerifyItemRequest> _requests = new();

    public Task SaveRequest(VerifyItemRequest request, CancellationToken cancellationToken)
    {
        _requests.Add(request);
        return Task.CompletedTask;
    }
}
