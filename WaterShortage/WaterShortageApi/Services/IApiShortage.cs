using WaterShortageApi.Models;

namespace WaterShortageApi.Services;

public interface IApiShortage
{
    public Task<ShortageResponse> GetShortageAsync(string siret);
}