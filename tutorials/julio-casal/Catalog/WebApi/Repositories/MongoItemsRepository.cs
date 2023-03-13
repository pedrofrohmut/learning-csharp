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

    public void CreateItem(Item newItem)
    {
        items.InsertOne(newItem);
    }

    public void DeleteItem(Guid id)
    {
        items.DeleteOne(x => x.Id == id);
    }

    public Item? GetItemById(Guid id)
    {
        return items.Find(x => x.Id == id).FirstOrDefault();
    }

    public IEnumerable<Item> GetItems()
    {
        return items.Find(new BsonDocument()).ToList();
    }

    public void UpdateItem(Item updatedItem)
    {
        var oldItem = items.AsQueryable().FirstOrDefault(x => x.Id == updatedItem.Id);
        if (oldItem != null) {
            var update = Builders<Item>.Update
                .Set(x => x.Name, updatedItem.Name)
                .Set(x => x.Price, updatedItem.Price);
            items.UpdateOne(x => x.Id == updatedItem.Id, update);
        }
    }
}
