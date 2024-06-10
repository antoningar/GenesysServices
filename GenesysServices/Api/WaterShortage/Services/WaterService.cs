using System.Text.Json;
using Api.WaterShortage.Models;

namespace Api.WaterShortage.Services;

public class WaterService(IHttpClientFactory httpClientFactory) : IWaterService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Water");
    public async Task<ShortageResponse> GetRegionGravityByCodeAsync(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("department code must be filled");
        }
        
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("departements");
        
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception("External water service down");
        }

        await using Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
        WaterShortageRegion[]? result = await JsonSerializer.DeserializeAsync<WaterShortageRegion[]>(contentStream);

        return result!
            .Where(r => string.Equals(r.code, code[..2]))
            .Select(r => new ShortageResponse(
                r.nom,
                !string.IsNullOrWhiteSpace(r.niveauGraviteMax),
                r.niveauGraviteMax))
            .First();
    }
}