using Catalog.WebApi.Entities;

namespace Catalog.WebApi.Repositories;

public interface IItemsRepository
{
    Task<IEnumerable<Item>> GetItemsAsync();
    Task<Item> GetItemByIdAsync(Guid id);
    Task CreateItemAsync(Item newItem);
    Task UpdateItemAsync(Item updatedItem);
    Task DeleteItemAsync(Guid id);
}
