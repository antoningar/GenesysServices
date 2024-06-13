using Api.Overdrawn.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OverdrawnSpecs;

public class OverdrawnApiFactory : WebApplicationFactory<Program>
{
    public IOverdrawnService OverdrawnService = new Mock<IOverdrawnService>().Object;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IOverdrawnService>(s => OverdrawnService);
        });
    }
}