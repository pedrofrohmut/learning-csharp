using Catalog.WebApi.Dtos;
using Catalog.WebApi.Entities;
using Catalog.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers;

[ApiController]
[Route("api/v1/items")]
public class ItemsController : ControllerBase
{
    private readonly IItemsRepository itemsRepository;

    public ItemsController(IItemsRepository itemsRepository)
    {
        this.itemsRepository = itemsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsAsync()
    {
        var itemDtos = (await itemsRepository.GetItemsAsync())
                                             .Select(item => item.AsDto())
                                             .ToList();
        return Ok(itemDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItemByIdAsync(Guid id)
    {
        var item = await itemsRepository.GetItemByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item.AsDto());
    }

    [HttpPost]
    public async Task<ActionResult> CreateItemAsync(CreateItemDto newItem)
    {
        var item = Item.FromCreateItemDto(newItem);
        await itemsRepository.CreateItemAsync(item);
        return new ObjectResult("") { StatusCode = 201 };
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto updatedItemDto)
    {
        var foundItem = await itemsRepository.GetItemByIdAsync(id);
        if (foundItem == null) return NotFound();
        // var updatedItem = new Item(item.Id,
        //                            updatedItemDto.Name,
        //                            updatedItemDto.Price,
        //                            item.CreatedAt);
        var updatedItem = foundItem with {
            Name = updatedItemDto.Name,
            Price = updatedItemDto.Price,
        };
        await itemsRepository.UpdateItemAsync(updatedItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItemAsync(Guid id)
    {
        var foundItem = await itemsRepository.GetItemByIdAsync(id);
        if (foundItem == null) return NotFound();
        await itemsRepository.DeleteItemAsync(id);
        return NoContent();
    }
}
