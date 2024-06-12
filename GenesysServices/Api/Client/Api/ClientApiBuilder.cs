using System.Net;
using Api.Client.Models;
using Api.Client.Services;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Api.Client.Api;

public static class ClientApiBuilder
{
    public static void ConfigureClient(this WebApplicationBuilder builder, IConfigurationRoot configuration)
    {
        builder.Services.Configure<ClientDatabaseSettings>(
            builder.Configuration.GetSection("ClientDatabase"));
        builder.Services.AddSingleton<IDataClientService, DataClientService>();
    }
    
    public static void MapClientEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        app.MapGet(
                "/api/v{apiVersion:apiVersion}/client",
                async Task<IResult> (string phoneNumber, IDataClientService service) =>
            {
                try
                {
                    string? clientId = await service.GetClientIdByPhoneNumberAsync(phoneNumber);
                    return TypedResults.Ok(new {clientId});
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
            .WithName("GetClientId")
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .Produces<string>()
            .WithOpenApi();
    }
}