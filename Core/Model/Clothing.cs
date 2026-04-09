using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Clothing
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ObjectId { get; set; }
    public string? Type { get; set; }
    public string? Size { get; set; }
    public string? Color { get; set; }
    public string? Status { get; set; }
    public string? Owner_id { get; set; }
    public string? ImageUrl { get; set; } // Matches the frontend Base64 string
}