namespace DealWithSpesification;

/// <summary>
/// As a customer
/// I want to verify an item
/// So that I know I will get a valid item when I buy it
/// </summary>
public class VerifyItemStory
{
    [Fact]
    public async Task ShouldShowOkWhenItemIsValid()
    {
        // Given
        var itemId = "abc";

        // When
        var webPageDriver = new WebPageDriver();
        await webPageDriver.GoToTheStore();
        await webPageDriver.VerifyItem(itemId);

        // Then
        await webPageDriver.AssertVerifyItemValidity(itemId);
    }

    [Fact]
    public async Task ShouldShowUnknownWhenItemIsNotFound()
    {
        // Given
        var webPageDriver = new WebPageDriver();
        var itemId = "123notfound4";
        await webPageDriver.SetupCentralTestDouble(itemId, isValid: false, message: "E001");

        // When
        await webPageDriver.GoToTheStore();
        await webPageDriver.VerifyItem(itemId);

        // Then
        await webPageDriver.AssertVerifyItemValidity(itemId, isValid: false, message: "Item not found");
    }

    [Fact(Skip = "Not implemented")]
    public Task ShouldShowAlreadySoldWhenItemIsSold() => Task.CompletedTask;
}
