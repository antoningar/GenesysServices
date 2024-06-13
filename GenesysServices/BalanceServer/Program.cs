using Overdrawn.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

WebApplication app = builder.Build();

app.MapGrpcService<BalanceService>();

app.MapGet("/", () => "");

app.Run();