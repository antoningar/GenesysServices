using WaterShortageApi.Models;

namespace WaterShortageApi.Services;

public class WaterService : IWaterService
{
    public async Task<ShortageResponse> GetRegionGravityByCodeAsync(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("department code must be filled");
        }
        throw new NotImplementedException();
    }
}