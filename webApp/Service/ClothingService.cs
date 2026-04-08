using webApp.Model;
using MongoDB.Driver;

namespace webApp.Service;


public class ClothingService
{
    private readonly IMongoCollection<Clothing> _clothing;

    public ClothingService()
    {
        var client = new MongoClient("mongodb+srv://group1user:group1@cluster01.e90p8p6.mongodb.net/");
        var database = client.GetDatabase("Workshop");

        _clothing = database.GetCollection<Clothing>("Clothing");
    }

    public async Task CreateClothing(Clothing item)
    {
        await _clothing.InsertOneAsync(item);
    }

    public async Task<List<Clothing>> GetAllClothing()
    {
        return await _clothing.Find(_ => true).ToListAsync();
    }
}