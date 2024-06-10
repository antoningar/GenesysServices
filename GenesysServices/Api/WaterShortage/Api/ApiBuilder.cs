using System.Net.Http.Headers;
using Api.WaterShortage.Services;

namespace Api.WaterShortage.Api;

public static class ApiBuilder
{
    public static void ConfigureWaterShortage(this WebApplicationBuilder builder, IConfigurationRoot configuration)
    {
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
    }
}