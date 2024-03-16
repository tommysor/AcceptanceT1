namespace DealWithSpesification;

/// <summary>
/// As a customer
/// I want to verify an item
/// So that I know I will get a valid item when I buy it
/// </summary>
public class VerifyItemStory
{
    [Fact(Skip = "Not implemented")]
    public async Task ShouldShowOkWhenItemIsValid()
    {
        // Given
        var itemId = "abc";

        // When
        var webPageDriver = new WebPageDriver();
        await webPageDriver.GoToTheStore();
        await webPageDriver.VerifyItem(itemId);

        // Then
        await webPageDriver.VerifyItemIsValid(itemId);
    }

    [Fact(Skip = "Not implemented")]
    public async Task ShouldShowUnknownWhenItemIsNotFound() { }

    [Fact(Skip = "Not implemented")]
    public async Task ShouldShowAlreadySoldWhenItemIsSold() { }
}
