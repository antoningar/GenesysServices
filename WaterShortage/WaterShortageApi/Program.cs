using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using WaterShortageApi.Models;
using WaterShortageApi.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISiretService, SiretService>();
builder.Services.AddSingleton<IWaterService, WaterService>();
builder.Services.AddSingleton<IApiShortage, ApiShortage>();

void ConfigureClient(HttpClient client)
{
    string key = configuration.GetSection("SiretApi:key").Value!;
    string secret = configuration.GetSection("SiretApi:secret").Value!;
    string authStr = $"{key}:{secret}";
    string encodedAuthStr = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authStr));

    client.BaseAddress = new Uri("https://api.insee.fr/");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedAuthStr);
}

builder.Services.AddHttpClient("Siret", ConfigureClient);

builder.Services.AddHttpClient("Water", client =>
{
    client.BaseAddress = new Uri("https://api.vigieau.beta.gouv.fr/api/");
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/watershortage", async Task<IResult> (string siret, IApiShortage service) =>
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
.Produces<ShortageResponse>()
.WithOpenApi();

app.Run();


public partial class Program
{
}