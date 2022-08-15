using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Data;
using DataAccess.DbAccess;
using DataAccess.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace JobTrackerDataManager.Tests;

public class ParseHubRunDataTests
{
    private readonly ISqlDataAccess _db = Substitute.For<ISqlDataAccess>();

    [Fact]
    public async void GetParseHubRuns_ReturnsRuns_WhenRunsExist()
    {
        // Arrange
        IEnumerable<ParseHubRunModel> runs = TestUtilities.GetParseHubRuns();
        _db.LoadData<ParseHubRunModel, dynamic>(Arg.Any<string>(), Arg.Any<Object>()).Returns(runs);
        var runData = new ParseHubRunData(_db);

        // Act
        var result = await runData.GetParseHubRuns();

        // Assert
        result.Should().BeEquivalentTo(runs);
    }

    [Fact]
    public async void GetParseHubRuns_ReturnsEmptyList_WhenRunsDoNotExist()
    {
        // Arrange
        IEnumerable<ParseHubRunModel> runs = new List<ParseHubRunModel>();
        _db.LoadData<ParseHubRunModel, dynamic>(Arg.Any<string>(), Arg.Any<Object>()).Returns(runs);
        var runData = new ParseHubRunData(_db);

        // Act
        var result = await runData.GetParseHubRuns();

        // Assert
        result.Count().Should().Be(0);
    }
}