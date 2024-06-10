using Api.ISBN.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ISBNSpecs;

public class ApiFactory : WebApplicationFactory<Program>
{
    public IIsbnService IsbnService = new Mock<IIsbnService>().Object;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IIsbnService>(s => IsbnService);
        });
    }
    
}