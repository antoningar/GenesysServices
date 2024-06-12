using Api.Client.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Client.Services;

public class DataClientService : IDataClientService
{
    private readonly IMongoCollection<Models.Client> _clientCollection;

    public DataClientService(IOptions<ClientDatabaseSettings> clientSettings)
    {
        MongoClient client = new(
            clientSettings.Value.ConnectionString);
        IMongoDatabase? database = client.GetDatabase(
            clientSettings.Value.DatabaseName);
        _clientCollection = database.GetCollection<Models.Client>(
            clientSettings.Value.ClientCollectionName);
    }

    public async Task<string?> GetClientIdByPhoneNumberAsync(string phoneNumber)
    {
        try
        {

            Models.Client result = await _clientCollection
                .Find(c => string.Equals(phoneNumber, c.Number, StringComparison.OrdinalIgnoreCase))
                .SingleAsync();

            return result.ClientId!;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}