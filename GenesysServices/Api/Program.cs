using Api.ISBN.Api;
using Api.WaterShortage.Api;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1);
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(options =>
    {
        //usefull for swagger
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });

builder.ConfigureWaterShortage(configuration);
builder.ConfigureIsbn(configuration);

WebApplication app = builder.Build();

ApiVersionSet versionSet = app
    .NewApiVersionSet()
    .HasApiVersion(1)
    .ReportApiVersions()
    .Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapWaterShortageEndpoints(versionSet);
app.MapIsbnEndpoints(versionSet);

app.Run();

public partial class Program
{
}