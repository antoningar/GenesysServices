using WaterShortageApi.Models;

namespace WaterShortageApi.Services;

public class ApiShortage(ISirenService _sirenService, IWaterService _waterService) : IApiShortage
{
    public async Task<ShortageResponse> GetShortageAsync(string siren)
    {
        string codeRegion = await _sirenService.GetCodeRegionBySirenAsync(siren);
        return await _waterService.GetRegionGravityByCodeAsync(codeRegion);
    }
}