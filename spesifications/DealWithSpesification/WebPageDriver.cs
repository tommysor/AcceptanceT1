using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace DealWithSpesification;

public class WebPageDriver
{
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;
    private IPage _page = null!;
    private readonly string _baseAddress;
    private readonly string _centralTestDoubleBaseAddress;

    public WebPageDriver()
    {
        _baseAddress = GetBaseAddress();
        _centralTestDoubleBaseAddress = GetCentralTestDoubleBaseAddress();
    }

    private static string GetBaseAddress()
    {
        var baseAddress = Environment.GetEnvironmentVariable("SPESIFICATIONS_BASEADDRESS") ?? throw new InvalidOperationException("SPESIFICATIONS_BASEADDRESS not found");
        baseAddress = AddSchemaIfNotPresent(baseAddress);
        return baseAddress;
    }

    private static string AddSchemaIfNotPresent(string baseAddress)
    {
        var isContainsSchema = baseAddress.Contains("http://") || baseAddress.Contains("https://");
        if (!isContainsSchema)
        {
            baseAddress = $"https://{baseAddress}";
        }

        return baseAddress;
    }

    private static string GetCentralTestDoubleBaseAddress()
    {
        var baseAddress = Environment.GetEnvironmentVariable("CENTRAL_TEST_DOUBLE_BASEADDRESS") ?? throw new InvalidOperationException("CENTRAL_TEST_DOUBLE_BASEADDRESS not found");
        baseAddress = AddSchemaIfNotPresent(baseAddress);
        return baseAddress;
    }

    public async Task SetupCentralTestDouble(string itemId, bool isValid, string message)
    {
        var client = new HttpClient { BaseAddress = new(_centralTestDoubleBaseAddress) };
        var response = await client.PostAsJsonAsync("central-verify-set-next-response", new
        {
            ItemId = itemId,
            IsValid = isValid,
            Message = message,
            ReferenceId = Guid.NewGuid(),
        });
        response.EnsureSuccessStatusCode();
    }

    public async Task GoToTheStore()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = true });

        _page = await _browser.NewPageAsync(new BrowserNewPageOptions
        {
            BaseURL = _baseAddress,
        });
        await _page.GotoAsync("/", new() { WaitUntil = WaitUntilState.NetworkIdle });
        await Task.Delay(5000);
        var h1 = await _page.WaitForSelectorAsync("h1");
        Assert.NotNull(h1);
        var innerHtml = await h1.InnerHTMLAsync();
        Assert.Equal("Welcome to the store", innerHtml);
    }

    public async Task VerifyItem(string itemId)
    {
        var verifyItemInput = _page.GetByLabel("Verify item");
        await verifyItemInput.FillAsync(itemId);
        var verifyButton = _page.GetByRole(AriaRole.Button, new() { Name = "Verify" });
        Assert.NotNull(verifyButton);
        await verifyButton.ClickAsync();
    }

    public async Task AssertVerifyItemValidity(string itemId, bool isValid = true, string message = "")
    {
        var verifyItemResult = await _page.WaitForSelectorAsync("#verify-item-result");
        Assert.NotNull(verifyItemResult);
        var resultItemId = _page.Locator("#verify-item-result-itemid");
        await Assertions.Expect(resultItemId).ToHaveTextAsync(itemId);
        var resultIsValid = _page.Locator("#verify-item-result-isvalid");
        await Assertions.Expect(resultIsValid).ToHaveTextAsync(isValid.ToString());
        var resultMessage = _page.Locator("#verify-item-result-message");
        await Assertions.Expect(resultMessage).ToHaveTextAsync(message);
    }
}
