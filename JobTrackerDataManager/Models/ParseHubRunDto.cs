using System.Reflection;
using JobTrackerDataManager.Utilities;

namespace JobTrackerDataManager.Models;

public record ParseHubRunDto(
    string? project_token, string? run_token, string? status, string? data_ready,
    string? start_time, string? end_time, string? pages, string? md5sum, string? start_url,
    string? start_template, string? start_value)
{
    public static async ValueTask<ParseHubRunDto?> BindAsync(HttpContext httpContext, ParameterInfo parameter)
    {
        return await httpContext.BindFromForm<ParseHubRunDto>();
    }
}