using DealWith.ApiService.Features.VerifyItemFeature.Contracts;
using DealWith.ApiService.Features.VerifyItemFeature.Infrastructure;

namespace DealWith.ApiService.Features.VerifyItemFeature;

public class VerifyItemService
{
    private readonly IStorage _storage;
    private readonly ICentral _central;

    public VerifyItemService(IStorage storage, ICentral central)
    {
        _storage = storage;
        _central = central;
    }

    public async Task<VerifyItemResponse> VerifyItem(VerifyItemRequest request, CancellationToken cancellationToken)
    {
        await _storage.SaveRequest(request, cancellationToken);
        var centralRequest = new CentralVerifyRequest(request.ItemId);
        var centralResponse = await _central.Verify(centralRequest, cancellationToken);
        var message = CentralMessageMapper.MapMessage(centralResponse.Message);
        var response = new VerifyItemResponse { ItemId = request.ItemId, IsValid = centralResponse.IsValid, Message = message };
        return response;
    }
}
