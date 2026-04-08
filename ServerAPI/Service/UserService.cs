using Core.Model;
using MongoDB.Driver;

namespace ServerAPI.Service;

public class UserService
{
    private readonly IMongoCollection<User> _users;

    public UserService()
    {
        var client = new MongoClient("mongodb+srv://group1user:group1@cluster01.e90p8p6.mongodb.net/");
        var database = client.GetDatabase("Workshop");

        _users = database.GetCollection<User>("Users");
    }

    public async Task CreateUser(User user)
    {
        await _users.InsertOneAsync(user);
    }
    public async Task<User> GetUserByUsername(string username)
    {
        return await _users.Find(u => u.Username == username).FirstOrDefaultAsync(); 
    }
}