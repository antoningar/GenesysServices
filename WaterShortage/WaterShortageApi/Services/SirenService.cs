namespace WaterShortageApi.Services;

public class SirenService : ISirenService
{
    public async Task<string> GetCodeRegionBySirenAsync(string siren)
    {
        if (string.IsNullOrWhiteSpace(siren))
        {
            throw new ArgumentException("Wrong siren parameter");
        }
        throw new NotImplementedException();
    }
}