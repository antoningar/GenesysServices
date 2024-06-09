namespace Api.WaterShortage.Services;

public interface ISiretService
{
    public Task<string> GetCodeRegionBySiretAsync(string siret);
}