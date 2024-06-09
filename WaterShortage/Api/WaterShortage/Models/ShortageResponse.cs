namespace Api.WaterShortage.Models;

public record ShortageResponse(string Department, bool IsShortage, string Gravity)
{
}