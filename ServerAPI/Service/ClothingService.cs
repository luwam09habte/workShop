using Core.Model;
using MongoDB.Driver;

namespace ServerAPI.Service;

// ændre så at man kan se de tilgængelige stykker tøj


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
    
    
    public async Task<List<Clothing>> GetAvailableClothes() =>
        await _clothing.Find(c => c.Status == "available").ToListAsync();

    public async Task<Clothing> GetClothingById(string id) =>
        await _clothing.Find(c => c.ObjectId == id).FirstOrDefaultAsync();

    public async Task<bool> LoanClothing(string clothingId, string userId)
    {
        var clothing = await GetClothingById(clothingId);
        if (clothing == null || clothing.Status != "available") return false;

        clothing.Status = "loaned";
        clothing.Owner_id = userId;

        var filter = Builders<Clothing>.Filter.Eq(c => c.ObjectId, clothingId);
        await _clothing.ReplaceOneAsync(filter, clothing);
        return true;
    }
}