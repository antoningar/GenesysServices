using Api.WaterShortage.Models;

namespace Api.WaterShortage.Services;

public interface IApiShortage
{
    public Task<ShortageResponse> GetShortageAsync(string siret);
}