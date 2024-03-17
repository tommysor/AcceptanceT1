using DealWith.ApiService.Features.VerifyItemFeature.Contracts;
using DealWith.ApiService.Features.VerifyItemFeature.Infrastructure;

namespace DealWith.ApiService.Features.VerifyItemFeature;

public class VerifyItemService
{
    private readonly IStorage _storage;
    public VerifyItemService(IStorage storage)
    {
        _storage = storage;
    }

    public async Task<VerifyItemResponse> VerifyItem(VerifyItemRequest request, CancellationToken cancellationToken)
    {
       await _storage.SaveRequest(request, cancellationToken);
       return new VerifyItemResponse { ItemId = request.ItemId, IsValid = true };
    }
}
