namespace Api.Overdrawn.Services;

public interface IOverdrawnService
{
    public float GetClientBalance(string clientId);
}