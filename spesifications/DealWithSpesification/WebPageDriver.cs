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
    }

    public async Task DummyCheck()
    {
        var element = await _page.WaitForSelectorAsync("h1");
        Assert.NotNull(element);
        var innerHtml = await element.InnerHTMLAsync();
        Assert.Equal("Welcome to the store", innerHtml);
        Assert.Fail("Not implemented yet");
    }
}
