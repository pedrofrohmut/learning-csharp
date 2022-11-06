using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories;

public class MongoDbItemsRepository : IItemsRepository
{
    private const string databaseName   = "catalog_db";
    private const string collectionName = "items";

    private readonly IMongoCollection<Item> items;

    public MongoDbItemsRepository(IMongoClient client)
    {
        var database = client.GetDatabase(databaseName);
        items = database.GetCollection<Item>(collectionName);
    }

    public void Create(Item newItem) { items.InsertOne(newItem); }

    public void DeleteItem(Guid id)
    {
        items.DeleteOne(item => item.Id == id);
    }

    public Item? GetItem(Guid id) => items.Find(item => item.Id == id).FirstOrDefault();

    public IEnumerable<Item> GetItems() => items.Find(new BsonDocument()).ToList();

    public void UpdateItem(Item updatedItem)
    {
        items.ReplaceOne(item => item.Id == updatedItem.Id, updatedItem);
    }
}
