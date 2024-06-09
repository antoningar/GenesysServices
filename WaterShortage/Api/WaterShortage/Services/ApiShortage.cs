using Api.WaterShortage.Models;

namespace Api.WaterShortage.Services;

public class ApiShortage(ISiretService siretService, IWaterService waterService) : IApiShortage
{
    public async Task<ShortageResponse> GetShortageAsync(string siret)
    {
        string codeRegion = await siretService.GetCodeRegionBySiretAsync(siret);
        return await waterService.GetRegionGravityByCodeAsync(codeRegion);
    }
}