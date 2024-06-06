using System.Net;
using Microsoft.AspNetCore.Mvc;
using WaterShortageApi.Models;
using WaterShortageApi.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISirenService, SirenService>();
builder.Services.AddSingleton<IWaterService, WaterService>();
builder.Services.AddSingleton<IApiShortage, ApiShortage>();
// builder.Services.AddSingleton<IHttpService, HttpService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/watershortage", async Task<IResult> (string siren, IApiShortage service) =>
{
    try
    {
        return TypedResults.Ok(await service.GetShortageAsync(siren));
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
.Produces<ShortageResponse>()
.WithOpenApi();

app.Run();


public partial class Program
{
}