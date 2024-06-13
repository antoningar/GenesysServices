namespace Api.Overdrawn.Services;

public interface IOverdrawnService
{
    public Task<float> GetClientBalanceAsync(string clientId);
}