using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Skinet.Core.Repositories;

namespace Skinet.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository productRepository;
    private readonly IProductBrandRepository productBrandRepository;
    private readonly IProductTypeRepository productTypeRepository;

    public ProductsController(IProductRepository productRepository,
                              IProductBrandRepository productBrandRepository,
                              IProductTypeRepository productTypeRepository)
    {
        this.productRepository = productRepository;
        this.productBrandRepository = productBrandRepository;
        this.productTypeRepository = productTypeRepository;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await this.productRepository.GetProducts();
        return new ObjectResult(products) { StatusCode = 200 };
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetSingleProduct([FromRoute] int productId)
    {
        var product = await this.productRepository.GetProductById(productId);
        return new ObjectResult(product) { StatusCode = 200 };
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetAllProductBrands()
    {
        var brands = await this.productBrandRepository.GetProductBrands();
        return new ObjectResult(brands) { StatusCode = 200 };
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetAllProductTypes()
    {
        var types = await this.productTypeRepository.GetProductTypes();
        return new ObjectResult(types) { StatusCode = 200 };
    }
}
