using DealWith.ApiService.Features.VerifyItemFeature.Contracts;

namespace DealWith.ApiService.Features.VerifyItemFeature.Infrastructure;

public interface ICentral
{
    Task<CentralVerifyResponse> Verify(CentralVerifyRequest request, CancellationToken cancellationToken);
}
