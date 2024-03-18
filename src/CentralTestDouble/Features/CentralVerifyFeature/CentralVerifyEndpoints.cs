using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TestDoubles.CentralTestDouble.Features.CentralVerifyFeature;

public static class CentralVerifyEndpoints
{
    public static Queue<CentralVerifyResponse> Responses { get; } = new();
    public static CentralVerifyResponse GetDefaultResponse(string itemId)
        => new(itemId, true, null, Guid.NewGuid());

    public static Ok<CentralVerifyResponse> VerifyItem(
        [FromBody] CentralVerifyRequest request
    )
    {
        if (Responses.TryDequeue(out var response))
        {
            return TypedResults.Ok(response);
        }
        var defaultResponse = GetDefaultResponse(request.ItemId);
        return TypedResults.Ok(defaultResponse);
    }

    public static Ok VerifyItemNextResponse(
        [FromBody] CentralVerifyResponse nextResponse
    )
    {
        Responses.Enqueue(nextResponse);
        return TypedResults.Ok();
    }
}
