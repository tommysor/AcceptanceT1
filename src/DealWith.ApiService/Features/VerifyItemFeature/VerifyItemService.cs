using DealWith.ApiService.Features.VerifyItemFeature.Contracts;

namespace DealWith.ApiService.Features.VerifyItemFeature;

public class VerifyItemService
{
    public VerifyItemService()
    {
    }

    public async Task<VerifyItemResponse> VerifyItem(VerifyItemRequest request, CancellationToken cancellationToken)
    {
       await Task.CompletedTask;
       return new VerifyItemResponse { ItemId = request.ItemId, IsValid = true };
    }
}
