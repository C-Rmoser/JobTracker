using System.Reflection;

namespace JobTrackerDataManager.Models;

public record ParseHubRunModel(
    string? project_token, string? run_token, string? status, string? data_ready,
    string? start_time, string? end_time, string? pages, string? md5sum, string? start_url,
    string? start_template, string? start_value)
{
    public static async ValueTask<ParseHubRunModel?> BindAsync(HttpContext httpContext, ParameterInfo parameter)
    {
        return await httpContext.BindFromForm<ParseHubRunModel>();
    }
}