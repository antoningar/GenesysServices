using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ClientSpecs;

public class ClientApiFactory : WebApplicationFactory<Program>
{    
    // public IDataClientService DataClientService = new Mock<IDataClientService>().Object;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // services.AddSingleton<IDataClientService>(s => DataClientService);
        });
    }
    
}