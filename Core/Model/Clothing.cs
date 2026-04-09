using MongoDB.Bson;

namespace Core.Model;

public class Clothing
{ 
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    public string Type { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public string Status { get; set; }
    public string Owner_id { get; set; }
    public string ImageUrl { get; set; }
}
