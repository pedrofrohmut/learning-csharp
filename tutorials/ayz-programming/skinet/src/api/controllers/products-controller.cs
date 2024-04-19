using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet.Infrastructure.Data;

namespace Skinet.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly StoreDbContext dbContext;

    public ProductsController(StoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllProducts()
    {
        if (this.dbContext.Products == null) return NotFound();
        var products = await this.dbContext.Products.ToListAsync();
        return new ObjectResult(products) { StatusCode = 200 };
    }

    [HttpGet("single/{productId}")]
    public async Task<IActionResult> GetSingleProduct([FromRoute] int productId)
    {
        if (this.dbContext.Products == null) return NotFound();
        var product = await this.dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
        return new ObjectResult(product) { StatusCode = 200 };
    }
}
