using WaterShortageApi.Models;

namespace WaterShortageApi.Services;

public class ApiShortage(ISiretService siretService, IWaterService _waterService) : IApiShortage
{
    public async Task<ShortageResponse> GetShortageAsync(string siret)
    {
        string codeRegion = await siretService.GetCodeRegionBySiretAsync(siret);
        return await _waterService.GetRegionGravityByCodeAsync(codeRegion);
    }
}