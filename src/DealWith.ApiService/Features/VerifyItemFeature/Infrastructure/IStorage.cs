using DealWith.ApiService.Features.VerifyItemFeature.Contracts;

namespace DealWith.ApiService.Features.VerifyItemFeature.Infrastructure
{
    public interface IStorage
    {
        Task SaveRequest(VerifyItemRequest request, CancellationToken cancellationToken);
    }
}