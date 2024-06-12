using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Client.Models;

public class Client
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("clientId")]
    public string? ClientId { get; set; }
    
    [BsonElement("number")]
    public string? Number { get; set; }
}