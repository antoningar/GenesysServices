using WaterShortageApi.Models;

namespace WaterShortageApi.Services;

public interface IWaterService
{
    public Task<ShortageResponse> GetRegionGravityByCodeAsync(string code);
}