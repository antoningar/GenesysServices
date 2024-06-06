using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WaterShortageApi.Services;

namespace WeatherShortageSpecs;

public class ApiFactory : WebApplicationFactory<Program>
{
    public ISiretService SiretService = new Mock<ISiretService>().Object;
    public IWaterService WaterService = new Mock<IWaterService>().Object;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<ISiretService>(s => SiretService);
            services.AddSingleton<IWaterService>(s => WaterService);
        });
    }
}