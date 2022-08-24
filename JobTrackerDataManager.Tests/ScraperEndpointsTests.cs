using System;
using System.Collections.Generic;
using DataAccess.Data;
using DataAccess.Models;
using FluentAssertions;
using JobTrackerDataManager.DTOs;
using JobTrackerDataManager.Endpoints;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace JobTrackerDataManager.Tests;

public class ScraperEndpointsTests
{
    private readonly IJobData _data = Substitute.For<IJobData>();

    [Fact]
    public async void KarriereAtWebHook_ReturnsOk_WhenArgumentIsValid()
    {
        // Arrange
        var dtos = Utilities.GetKarriereAtDtos();
        _data.InsertJobs(Arg.Any<List<JobModel>>()).Returns(1);

        // Act
        var result = await ScraperEndpoints.KarriereAtWebHook(dtos, _data);

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public async void DevJobsAtWebHook_ReturnsOk_WhenArgumentIsValid()
    {
        // Arrange
        var dtos = Utilities.GetDevJobsAtDtos();
        _data.InsertJobs(Arg.Any<List<JobModel>>()).Returns(1);

        // Act
        var result = await ScraperEndpoints.DevJobsAtWebHook(dtos, _data);

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public async void KarriereAtWebHook_ReturnsBadRequest_WhenArgumentIsNotValid()
    {
        // Arrange
        var dtos = new List<KarriereAtDto>();
        _data.InsertJobs(Arg.Any<List<JobModel>>()).Returns(0);

        // Act
        var result = await ScraperEndpoints.KarriereAtWebHook(dtos, _data);

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(400);
    }

    [Fact]
    public async void DevJobsAtWebHook_ReturnsBadRequest_WhenArgumentIsNotValid()
    {
        // Arrange
        var dtos = new List<DevJobsDto>();
        _data.InsertJobs(Arg.Any<List<JobModel>>()).Returns(0);

        // Act
        var result = await ScraperEndpoints.DevJobsAtWebHook(dtos, _data);

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(400);
    }

    [Fact]
    public async void KarriereAtWebHook_ReturnsInternalServerError_WhenDatabaseInsertionFails()
    {
        // Arrange
        var dtos = Utilities.GetKarriereAtDtos();
        _data.InsertJobs(Arg.Any<List<JobModel>>()).Throws(new ApplicationException());

        // Act
        var result = await ScraperEndpoints.KarriereAtWebHook(dtos, _data);

        // Assert
        var value = result.GetProblemObject();
        value.Status.Should().Be(500);
    }

    [Fact]
    public async void DevJobsAtWebHook_ReturnsInternalServerError_WhenDatabaseInsertionFails()
    {
        // Arrange
        var dtos = Utilities.GetDevJobsAtDtos();
        _data.InsertJobs(Arg.Any<List<JobModel>>()).Throws(new ApplicationException());

        // Act
        var result = await ScraperEndpoints.DevJobsAtWebHook(dtos, _data);

        // Assert
        var value = result.GetProblemObject();
        value.Status.Should().Be(500);
    }
}