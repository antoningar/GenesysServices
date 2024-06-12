namespace Api.Client.Models;

public record ClientDatabaseSettings(
    string ConnectionString,
    string DatabaseName,
    string ClientCollectionName)
{
}