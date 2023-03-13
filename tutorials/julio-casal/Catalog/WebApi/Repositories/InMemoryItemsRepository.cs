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

    public IEnumerable<Item> GetItems() => items;

    public Item? GetItemById(Guid id) => items.FirstOrDefault<Item>(x => x.Id == id);

    public void CreateItem(Item item)
    {
        items.Add(item);
    }

    public void UpdateItem(Item updatedItem)
    {
        var oldItem = items.FirstOrDefault(x => x.Id == updatedItem.Id);
        if (oldItem != null) {
            items.Remove(oldItem);
        }
        items.Add(updatedItem);
    }

    public void DeleteItem(Guid id)
    {
        var itemToDelete = items.FirstOrDefault(x => x.Id == id);
        if (itemToDelete != null) items.Remove(itemToDelete);
    }
}
