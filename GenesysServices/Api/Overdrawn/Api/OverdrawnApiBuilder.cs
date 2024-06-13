using System.Net;
using Api.Overdrawn.Services;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Api.Overdrawn.Api;

public static class OverdrawnApiBuilder
{
    public static void ConfigureOverdrawn(this WebApplicationBuilder builder, IConfigurationRoot configuration)
    {
        builder.Services.AddSingleton<IOverdrawnService, OverdrawnService>();
    }
    
    public static void MapOverdrawnEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        app.MapGet("/api/v{apiVersion:apiVersion}/overdrawn", IResult (string clientId, IOverdrawnService service) =>
            {
                try
                {
                    float balance = service.GetClientBalance(clientId);
                    bool isOverdrawn = balance < 0;
                    return TypedResults.Ok(new {isOverdrawn});
                }
                catch (ArgumentException e)
                {
                    return Results.BadRequest(e.Message);
                }
                catch (Exception e)
                {
                    return Results.Problem(new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Detail = e.Message
                    });
                }
            })
            .WithName("IsOverdrawn")
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .Produces<bool>()
            .WithOpenApi();
    }
}