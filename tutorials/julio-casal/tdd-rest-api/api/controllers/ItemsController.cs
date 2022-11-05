using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers;

[ApiController]
[Route("api/items")]
public class ItemsController : ControllerBase
{
    private readonly IItemsRepository itemsRepository;

    public ItemsController(IItemsRepository itemsRepository)
    {
        this.itemsRepository = itemsRepository;
    }

    [HttpGet("")]
    public ActionResult<IEnumerable<ItemDto>> GetItems() =>
        Ok(this.itemsRepository.GetItems().Select(item => item.AsDto()));

    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetItem(Guid id)
    {
        var item = this.itemsRepository.GetItem(id);
        if (item == null) {
            return NotFound("Item not found!");
        }
        return Ok(item.AsDto());
    }

    [HttpPost("")]
    public ActionResult Create([FromBody] CreateItemDto newItem)
    {
        var item = new Item() {
            Id = Guid.NewGuid(),
            Name = newItem.Name,
            Price = newItem.Price,
            CreatedAt = DateTimeOffset.UtcNow
        };
        this.itemsRepository.Create(item);
        return new ObjectResult("") { StatusCode = 201 };
    }

    [HttpPut("{id}")]
    public ActionResult Update(Guid id, [FromBody] UpdateItemDto updatedItemDto)
    {
        var item = this.itemsRepository.GetItem(id);
        if (item is null) return NotFound();
        var updatedItem = new Item() {
            Id = id,
            Name = updatedItemDto.Name,
            Price = updatedItemDto.Price
        };
        this.itemsRepository.UpdateItem(updatedItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        this.itemsRepository.DeleteItem(id);
        return NoContent();
    }
}
