using System.Text.Json;
using WaterShortageApi.Models;

namespace WaterShortageApi.Services;

public class WaterService(IHttpClientFactory _httpClientFactory) : IWaterService
{
    private readonly HttpClient _httpClient = _httpClientFactory.CreateClient("Water");
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
            .Where(r => string.Equals(r.code, code.Substring(0, 2)))
            .Select(r => new ShortageResponse(
                r.nom,
                !string.IsNullOrWhiteSpace(r.niveauGraviteMax),
                r.niveauGraviteMax))
            .First();
    }
}