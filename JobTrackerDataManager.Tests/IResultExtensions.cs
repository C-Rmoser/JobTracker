using System;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobTrackerDataManager.Tests;

/// <summary>
/// These extensions are the only way to properly unit test API endpoints in .NET 6 minimal APIs
/// This has been in fixed in .NET 7
/// </summary>
/// <see href="https://www.youtube.com/watch?v=VuFQtyRmS0E">Problem in .NET 6</see>
/// <see href="https://www.youtube.com/watch?v=-i4rP0LGY5U">Fixed in .NET 7</see>
public static class IResultExtensions
{
    public static T? GetOkObjectResultValue<T>(this IResult result)
    {
        return (T?) Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("Value",
                BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.Public)?
            .GetValue(result);
    }

    public static int? GetOkObjectResultStatusCode(this IResult result)
    {
        return (int?) Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")
            ?
            .GetProperty("StatusCode",
                BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.Public)?
            .GetValue(result);
    }

    public static int? GetConflictObjectResultStatusCode(this IResult result)
    {
        return (int?) Type
            .GetType("Microsoft.AspNetCore.Http.Result.ConflictObjectResult, Microsoft.AspNetCore.Http.Results")
            ?
            .GetProperty("StatusCode",
                BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.Public)?
            .GetValue(result);
    }

    public static int? GetNotFoundResultStatusCode(this IResult result)
    {
        return (int?) Type
            .GetType("Microsoft.AspNetCore.Http.Result.NotFoundObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("StatusCode",
                BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.Public)?
            .GetValue(result);
    }

    public static ProblemDetails? GetProblemObject(this IResult result)
    {
        return (ProblemDetails?) Type
            .GetType("Microsoft.AspNetCore.Http.Result.ObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("Value",
                BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.Public)?
            .GetValue(result);
    }
}