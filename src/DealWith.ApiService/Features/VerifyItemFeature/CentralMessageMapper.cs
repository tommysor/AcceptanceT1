namespace DealWith.ApiService.Features.VerifyItemFeature;

public static class CentralMessageMapper
{
    public static string MapMessage(string? centralMessage)
    {
        return centralMessage switch
        {
            null => "",
            "" => "",
            "E001" => "Item not found",
            "W123" => "Item already sold",
            _ => "Unknown",
        };
    }
}
