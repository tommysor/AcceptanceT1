namespace DealWith.ApiService.Features.VerifyItemFeature.Contracts
{
    public class VerifyItemResponse
    {
        public required string ItemId { get; set; }
        public bool IsValid { get; set; }
        public string? Message { get; set; }
    }
}
