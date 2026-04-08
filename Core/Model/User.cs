using MongoDB.Bson;

namespace Core.Model;

public class User
{
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    /*public string Name { get; set; }*/
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}