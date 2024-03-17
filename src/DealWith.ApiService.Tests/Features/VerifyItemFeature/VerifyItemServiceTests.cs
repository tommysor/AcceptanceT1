using DealWith.ApiService.Features.VerifyItemFeature;
using DealWith.ApiService.Features.VerifyItemFeature.Contracts;
using DealWith.ApiService.Features.VerifyItemFeature.Infrastructure;
using NSubstitute;

namespace DealWith.ApiService.Tests.Features.VerifyItemFeature;

public class VerifyItemServiceTests
{
    private readonly VerifyItemService _sut;
    private readonly Bogus.Faker _faker;
    private readonly IStorage _storageMock;
    private readonly ICentral _centralMock;

    public VerifyItemServiceTests()
    {
        _faker = new Bogus.Faker();
        _storageMock = Substitute.For<IStorage>();
        _centralMock = Substitute.For<ICentral>();
        _sut = new VerifyItemService(_storageMock, _centralMock);
        var defaultCentralResponse = new CentralVerifyResponse("", true, "", Guid.Empty);
        _centralMock
            .Verify(Arg.Any<CentralVerifyRequest>(), Arg.Any<CancellationToken>())
            .Returns(defaultCentralResponse);
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

    [Fact]
    public async Task ShouldSaveRequest()
    {
        // Given
        var itemId = "123";
        var request = new VerifyItemRequest { ItemId = itemId };

        // When
        await _sut.VerifyItem(request, CancellationToken.None);

        // Then
        await _storageMock
            .Received(1)
            .SaveRequest(
                Arg.Is<VerifyItemRequest>(x => x.ItemId == itemId),
                Arg.Any<CancellationToken>()
                );
    }

    [Fact]
    public async Task ShouldReturnNotValidWhenCentralReturnsNotValid()
    {
        // Given
        var request = new VerifyItemRequest { ItemId = "123" };
        var centralResponse = new CentralVerifyResponse("", false, null, Guid.Empty);
        _centralMock
            .Verify(Arg.Any<CentralVerifyRequest>(), Arg.Any<CancellationToken>())
            .Returns(centralResponse);

        // When
        var response = await _sut.VerifyItem(request, CancellationToken.None);

        // Then
        Assert.False(response.IsValid);
    }

    [Fact]
    public async Task ShouldReturnValidWhenCentralReturnsValid()
    {
        // Given
        var request = new VerifyItemRequest { ItemId = "123" };
        var centralResponse = new CentralVerifyResponse("", true, "", Guid.Empty);
        _centralMock
            .Verify(Arg.Any<CentralVerifyRequest>(), Arg.Any<CancellationToken>())
            .Returns(centralResponse);

        // When
        var response = await _sut.VerifyItem(request, CancellationToken.None);

        // Then
        Assert.True(response.IsValid);
    }
}
