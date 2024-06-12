namespace Api.Client.Models;

public class ClientDatabaseSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? ClientCollectionName { get; set; }
}