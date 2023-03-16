using Catalog.WebApi.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.WebApi.Repositories;

public class MongoItemsRepository : IItemsRepository
{
    private const string DATABASE_NAME = "catalog";
    private const string COLLECTION_NAME = "items";

    private readonly IMongoCollection<Item> items;

    public MongoItemsRepository(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(DATABASE_NAME);
        items = database.GetCollection<Item>(COLLECTION_NAME);
    }

    public Task CreateItemAsync(Item newItem)
    {
        return items.InsertOneAsync(newItem);
    }

    public Task DeleteItemAsync(Guid id)
    {
        return items.DeleteOneAsync(x => x.Id == id);
    }

    public Task<Item> GetItemByIdAsync(Guid id)
    {
        return items.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
        return await items.Find(new BsonDocument()).ToListAsync();
    }

    public Task UpdateItemAsync(Item updatedItem)
    {
        var update = Builders<Item>.Update
            .Set(x => x.Name, updatedItem.Name)
            .Set(x => x.Price, updatedItem.Price);
        return items.UpdateOneAsync(x => x.Id == updatedItem.Id, update);
    }
}
