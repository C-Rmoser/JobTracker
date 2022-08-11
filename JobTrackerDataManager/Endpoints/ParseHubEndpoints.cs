using JobTrackerDataManager.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace JobTrackerDataManager.Endpoints;

public static class ParseHubEndpoints
{
    public static void ConfigureParseHubEndpoints(this WebApplication app)
    {
        // app.MapPost("/ParseHub", ProjectRunStatusChanged).AllowAnonymous();
        app.MapPost("/ParseHub", [Consumes("application/x-www-form-urlencoded")](HttpContext ctx) =>
        {
            var dto = new ParseHubRunModel
            {
                project_token = ctx.Request.Form["project_token"],
                run_token = ctx.Request.Form["run_token"],
                status = ctx.Request.Form["status"],
                data_ready = ctx.Request.Form["data_ready"],
                start_time = ctx.Request.Form["start_time"],
                end_time = ctx.Request.Form["end_time"],
                pages = ctx.Request.Form["pages"],
                md5sum = ctx.Request.Form["md5sum"],
                start_url = ctx.Request.Form["start_url"],
                start_template = ctx.Request.Form["start_template"],
                start_value = ctx.Request.Form["start_value"]
            };

            Log.Information("Received Object: {@dto}", dto);

            return Results.Ok();
        }).AllowAnonymous();
    }
}