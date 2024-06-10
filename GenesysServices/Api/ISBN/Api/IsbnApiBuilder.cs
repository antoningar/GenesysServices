using System.Net;
using Api.ISBN.Services;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Api.ISBN.Api;

public static class IsbnApiBuilder
{
    public static void ConfigureIsbn(this WebApplicationBuilder builder, IConfigurationRoot configuration)
    {
        builder.Services.AddSingleton<IIsbnService, IsbnService>();
    }
    
    public static void MapIsbnEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        app.MapGet("/api/v{apiVersion:apiVersion}/isbn", async Task<IResult> (string isbn, IIsbnService service) =>
        {
            try
            {
                bool isValid = await service.CheckIsbnAsync(isbn);
                return TypedResults.Ok(new {isValid});
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
        .WithName("CheckIsbn")
        .WithApiVersionSet(versionSet)
        .MapToApiVersion(1)
        .Produces<bool>()
        .WithOpenApi();
    }
}