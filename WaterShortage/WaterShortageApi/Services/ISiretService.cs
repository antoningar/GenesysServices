namespace WaterShortageApi.Services;

public interface ISiretService
{
    public Task<string> GetCodeRegionBySiretAsync(string siret);
}