namespace WaterShortageApi.Services;

public interface ISirenService
{
    public Task<string> GetCodeRegionBySirenAsync(string siren);
}