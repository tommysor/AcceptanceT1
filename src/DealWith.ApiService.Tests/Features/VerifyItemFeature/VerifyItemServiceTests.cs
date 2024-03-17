using DealWith.ApiService.Features.VerifyItemFeature;
using DealWith.ApiService.Features.VerifyItemFeature.Contracts;

namespace DealWith.ApiService.Tests.Features.VerifyItemFeature;

public class VerifyItemServiceTests
{
    private readonly VerifyItemService _sut;
    private readonly Bogus.Faker _faker;

    public VerifyItemServiceTests()
    {
        _faker = new Bogus.Faker();
        _sut = new VerifyItemService();
    }

    [Fact]
    public async Task ShouldReturnSameItemId()
    {
        // Given
        var itemId = _faker.Random.Guid().ToString();
        var request = new VerifyItemRequest { ItemId = itemId };

        // When
        var response = await _sut.VerifyItem(request, CancellationToken.None);

        // Then
        Assert.Equal(itemId, response.ItemId);
    }
}
