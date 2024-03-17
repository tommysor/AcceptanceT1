using DealWith.ApiService.Features.VerifyItemFeature.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DealWith.ApiService.Features.VerifyItemFeature;

public static class VerifyItemEndpoints
{
    public static async Task<Ok<VerifyItemResponse>> VerifyItem(
        [FromBody] VerifyItemRequest request,
        [FromServices] VerifyItemService service,
        CancellationToken cancellationToken)
    {
        var response = await service.VerifyItem(request, cancellationToken);
        return TypedResults.Ok(response);
    }
}
