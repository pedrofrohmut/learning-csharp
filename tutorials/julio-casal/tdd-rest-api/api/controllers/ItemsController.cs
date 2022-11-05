using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers;

[ApiController]
[Route("api/items")]
public class ItemsController : ControllerBase
{
    private readonly InMemoryItemsRepository inMemoryItemsRepository;

    public ItemsController(InMemoryItemsRepository inMemoryItemsRepository)
    {
        this.inMemoryItemsRepository = inMemoryItemsRepository;
    }

    [HttpGet("")]
    public ActionResult<IEnumerable<Item>> GetItems() => Ok(this.inMemoryItemsRepository.GetItems());

    [HttpGet("{id}")]
    public ActionResult<Item> GetItem(Guid id)
    {
        var item = this.inMemoryItemsRepository.GetItem(id);
        if (item == null) {
            return NotFound("Item not found!");
        }
        return Ok(item);
    }
}
