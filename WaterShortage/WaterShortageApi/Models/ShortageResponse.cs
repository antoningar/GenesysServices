namespace WaterShortageApi.Models;

public record ShortageResponse(string Department, bool IsShortage, string Gravity)
{
}