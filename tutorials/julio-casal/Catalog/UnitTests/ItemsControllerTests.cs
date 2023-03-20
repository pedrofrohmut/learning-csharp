using System;
using Catalog.WebApi.Controllers;
using Catalog.WebApi.Dtos;
using Catalog.WebApi.Entities;
using Catalog.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Catalog.UnitTests;

public class ItemsControllerTests
{
    /*
        Noob = Arrange / Act / Assert
        Pro  = Given / When / Then
        */

    [Fact]
    public async void GetItemByIdAsync_WithUnexistingItem_ReturnsNotFound()
    {
        // Arrange
        var itemsRepository = new Mock<IItemsRepository>(); // Mock without setup returns null by default
        var itemsController = new ItemsController(itemsRepository.Object);

        // Act
        var result = await itemsController.GetItemByIdAsync(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async void GetItemByIdAsync_WithExistingItem_ReturnsOkAndItem()
    {
        // Arrange
        var itemsRepository = new Mock<IItemsRepository>();

        var itemDb = new Item(id: Guid.NewGuid(),
                              name: "Milk",
                              price: 2.99m,
                              createdAt: DateTimeOffset.UtcNow);

        itemsRepository.Setup(x => x.GetItemByIdAsync(It.IsAny<Guid>())).ReturnsAsync(itemDb);

        var itemsController = new ItemsController(itemsRepository.Object);

        // Act
        var result = await itemsController.GetItemByIdAsync(Guid.NewGuid());

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
        // Assert.IsType<ItemDto>(result.Value);

        Console.WriteLine($"Result: {result.Result}");
        Console.WriteLine($"Value is Null? {result.Value == null}");

        var resultItem = result.Value;

        // Console.WriteLine($"Result is null {result == null}");
        // Console.WriteLine($"Result.Value is null {result.Value == null}");
        // Assert.Equal(resultItem!.Id, itemDb.Id);
        // Assert.Equal(resultItem!.Name, itemDb.Name);
        // Assert.Equal(resultItem!.Price, itemDb.Price);
    }
}
