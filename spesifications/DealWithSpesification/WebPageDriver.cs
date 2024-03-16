using System.Threading.Tasks;
using Microsoft.Playwright;

namespace DealWithSpesification;

public class WebPageDriver
{
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;
    private IPage _page = null!;
    private string _baseAddress = null!;

    public async Task GoToTheStore()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = true });
        _baseAddress = Environment.GetEnvironmentVariable("SPESIFICATIONS_BASEADDRESS") ?? throw new InvalidOperationException("SPESIFICATIONS_BASEADDRESS not found");
        var isContainsSchema = _baseAddress.Contains("http://") || _baseAddress.Contains("https://");
        if (!isContainsSchema)
        {
            _baseAddress = $"https://{_baseAddress}";
        }
        _page = await _browser.NewPageAsync(new BrowserNewPageOptions
        {
            BaseURL = _baseAddress,
        });
        await _page.GotoAsync("/");
        var h1 = await _page.WaitForSelectorAsync("h1");
        Assert.NotNull(h1);
        var innerHtml = await h1.InnerHTMLAsync();
        Assert.Equal("Welcome to the store", innerHtml);
    }

    public async Task VerifyItem(string itemId)
    {
        var verifyItemInput = _page.GetByLabel("Verify item");
        Assert.NotNull(verifyItemInput);
        await verifyItemInput.FillAsync(itemId);
        var verifyButton = _page.GetByText("Verify");
        Assert.NotNull(verifyButton);
        await verifyButton.ClickAsync();
    }

    public async Task VerifyItemIsValid(string itemId)
    {
        var verifyItemResult = await _page.WaitForSelectorAsync("#verify-item-result");
        Assert.NotNull(verifyItemResult);
        var innerHtml = await verifyItemResult.InnerHTMLAsync();
        Assert.Contains(itemId, innerHtml);
        Assert.Contains("is valid", innerHtml);
    }
}
