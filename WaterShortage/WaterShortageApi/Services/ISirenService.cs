namespace WaterShortageApi.Services;

public interface ISirenService
{
    public string GetCodeRegionBySirenAsync(string siren);
}