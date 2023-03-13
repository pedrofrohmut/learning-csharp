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
    public ActionResult<IEnumerable<ItemDto>> GetItems()
    {
        var itemDtos = itemsRepository
            .GetItems()
            .Select(item => item.AsDto())
            .ToList();
        return Ok(itemDtos);
    }

    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetItById(string id)
    {
        var item =  itemsRepository.GetItemById(Guid.Parse(id));
        if (item == null) return NotFound();
        var itemDto = item.AsDto();
        return Ok(itemDto);
    }

    [HttpPost]
    public ActionResult CreateItem(CreateItemDto newItem)
    {
        Item item = Item.FromCreateItemDto(newItem);
        itemsRepository.CreateItem(item);
        return new ObjectResult("") { StatusCode = 201 };
    }

    [HttpPut("{id}")]
    public ActionResult UpdateItem(Guid id, UpdateItemDto updatedItemDto)
    {
        var foundItem = itemsRepository.GetItemById(id);
        if (foundItem == null) return NotFound();
        // var updatedItem = new Item(item.Id,
        //                            updatedItemDto.Name,
        //                            updatedItemDto.Price,
        //                            item.CreatedAt);
        var updatedItem = foundItem with {
            Name = updatedItemDto.Name,
            Price = updatedItemDto.Price,
        };
        itemsRepository.UpdateItem(updatedItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteItem(Guid id)
    {
        var foundItem = itemsRepository.GetItemById(id);
        if (foundItem == null) return NotFound();
        itemsRepository.DeleteItem(id);
        return NoContent();
    }
}
