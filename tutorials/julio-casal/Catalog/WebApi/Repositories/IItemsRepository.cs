using Catalog.WebApi.Entities;

namespace Catalog.WebApi.Repositories;

public interface IItemsRepository
{
    IEnumerable<Item> GetItems();
    Item? GetItemById(Guid id);
    void CreateItem(Item newItem);
    void UpdateItem(Item updatedItem);
    void DeleteItem(Guid id);
}
