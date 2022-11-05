using Catalog.Entities;

namespace Catalog.Repositories;

public class InMemoryItemsRepository : IItemsRepository
{
    private readonly List<Item> items;

    public InMemoryItemsRepository()
    {
        this.items = new List<Item>() {
            new Item() { Name = "Potion"       , Price = 9 },
            new Item() { Name = "Iron Sword"   , Price = 20 },
            new Item() { Name = "Bronze Shield", Price = 18 },
        };
    }


    public IEnumerable<Item> GetItems() => items;

    public Item? GetItem(Guid id) => items.Where(item => item.Id == id).FirstOrDefault();

    public void Create(Item newItem)
    {
        items.Add(newItem);
    }

    public void UpdateItem(Item updatedItem)
    {
        var index = items.FindIndex(item => item.Id == updatedItem.Id);
        items[index] = updatedItem;
    }

    public void DeleteItem(Guid id)
    {
        var index = items.FindIndex(item => item.Id == id);
        items.RemoveAt(index);
    }
}
