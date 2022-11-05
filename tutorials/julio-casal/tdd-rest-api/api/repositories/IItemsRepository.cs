using Catalog.Entities;

namespace Catalog.Repositories;

public interface IItemsRepository
{
    IEnumerable<Item> GetItems();
    Item? GetItem(Guid id);
    void Create(Item newItem);
    void UpdateItem(Item updatedItem);
    void DeleteItem(Guid id);
}
