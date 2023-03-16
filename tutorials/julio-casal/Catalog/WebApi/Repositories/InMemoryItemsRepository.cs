using Catalog.WebApi.Entities;

namespace Catalog.WebApi.Repositories;

public class InMemoryItemsRepository : IItemsRepository
{
    private readonly List<Item> items;

    public InMemoryItemsRepository()
    {
        items = new List<Item>() {
            new Item(name: "Potion",       price: 9),
            new Item(name: "Iron Sword",   price: 20),
            new Item(name: "Broze Shield", price: 18),
        };
    }

    public Task<IEnumerable<Item>> GetItemsAsync() =>
        Task.FromResult((IEnumerable<Item>) items);

    public Task<Item> GetItemByIdAsync(Guid id) =>
        Task.FromResult(items.SingleOrDefault(x => x.Id == id)!);

    public Task CreateItemAsync(Item item)
    {
        items.Add(item);
        return Task.CompletedTask;
    }

    public Task UpdateItemAsync(Item updatedItem)
    {
        var oldItem = items.FirstOrDefault(x => x.Id == updatedItem.Id);
        if (oldItem != null) {
            items.Remove(oldItem);
        }
        items.Add(updatedItem);
        return Task.CompletedTask;
    }

    public Task DeleteItemAsync(Guid id)
    {
        var itemToDelete = items.FirstOrDefault(x => x.Id == id);
        if (itemToDelete != null) items.Remove(itemToDelete);
        return Task.CompletedTask;
    }
}
