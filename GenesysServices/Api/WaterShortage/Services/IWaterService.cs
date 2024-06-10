using Api.WaterShortage.Models;

namespace Api.WaterShortage.Services;

public interface IWaterService
{
    public Task<ShortageResponse> GetRegionGravityByCodeAsync(string code);
}