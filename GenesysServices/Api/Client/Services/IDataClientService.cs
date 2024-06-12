namespace Api.Client.Services;

public interface IDataClientService
{
    public Task<string?> GetClientIdByPhoneNumberAsync(string phoneNumber);
}