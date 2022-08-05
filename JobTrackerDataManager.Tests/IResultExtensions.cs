using System;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace JobTrackerDataManager.Tests;

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
}