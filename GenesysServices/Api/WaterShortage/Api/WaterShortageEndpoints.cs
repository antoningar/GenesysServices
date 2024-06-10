using System.Net;
using Api.WaterShortage.Models;
using Api.WaterShortage.Services;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Api.WaterShortage.Api;

public static class WaterShortageEndpoints
{
    public static void MapWaterShortageEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        app.MapGet("/api/v{apiVersion:apiVersion}/watershortage", async Task<IResult> (string siret, IApiShortage service) =>
            {
                try
                {
                    return TypedResults.Ok(await service.GetShortageAsync(siret));
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
            .WithName("GetWaterShortage")
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .Produces<ShortageResponse>()
            .WithOpenApi();
    }
}