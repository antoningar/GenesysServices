using System.Net.Http.Headers;
using System.Text.Json;

namespace Api.WaterShortage.Services;

public class SiretService(IHttpClientFactory httpClientFactory) : ISiretService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Siret");
    public async Task<string> GetCodeRegionBySiretAsync(string siret)
    {
        if (string.IsNullOrWhiteSpace(siret))
        {
            throw new ArgumentException("Wrong siret parameter");
        }
        
        if (!string.Equals("Bearer", _httpClient.DefaultRequestHeaders.Authorization!.Scheme))
        {
            await SetTokenAsync();
        }
        
        return await GetCodeRegion(siret);
    }

    private async Task<string> GetCodeRegion(string siret)
    {
        HttpResponseMessage httpResponseMessage =
            await _httpClient.GetAsync($"entreprises/sirene/V3.11/siret/{siret}?champs=codePostalEtablissement");
        
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            return string.Empty;
        }

        await using Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
        JsonElement result = await JsonSerializer.DeserializeAsync<JsonElement>(contentStream);
        return result
            .GetProperty("etablissement")
            .GetProperty("adresseEtablissement")
            .GetProperty("codePostalEtablissement")
            .ToString();
    }

    private async Task SetTokenAsync()
    {
        FormUrlEncodedContent stringContent = new(new Dictionary<string, string>
        {
            {"grant_type", "client_credentials"}
        });
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync("token", stringContent);
    
        await using Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
        JsonElement result = await JsonSerializer.DeserializeAsync<JsonElement>(contentStream);
        string token = result.GetProperty("access_token").ToString();
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}