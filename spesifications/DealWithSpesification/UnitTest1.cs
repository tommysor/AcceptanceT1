namespace DealWithSpesification;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var webPageDriver = new WebPageDriver();
        await webPageDriver.GoToTheStore();
        await webPageDriver.DummyCheck();
    }
}
