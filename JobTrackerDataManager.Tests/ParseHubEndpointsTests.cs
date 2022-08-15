using DataAccess.Data;
using DataAccess.Models;
using FluentAssertions;
using JobTrackerDataManager.Endpoints;
using JobTrackerDataManager.Models;
using NSubstitute;
using Xunit;

namespace JobTrackerDataManager.Tests;

public class ParseHubEndpointsTests
{
    private readonly IParseHubRunData _data = Substitute.For<IParseHubRunData>();

    [Fact]
    public async void ParseHubWebHook_ReturnsOk_OnSuccess()
    {
        // Arrange
        _data.InsertParseHubRun(Arg.Any<ParseHubRunModel>()).Returns(1);

        // Act
        var result =
            await (ParseHubEndpoints.ParseHubWebHook(
                new ParseHubRunDto(null, null, null, null, null, null, null, null, null, null, null), _data));

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }
}