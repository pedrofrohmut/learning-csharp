using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Repositories;
using Skinet.Infrastructure.Data;

namespace Skinet.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly StoreDbContext dbContext;

    public ProductRepository(StoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Product?> GetProductById(int productId)
    {
        if (this.dbContext.Products == null) return null;
        return await this.dbContext.Products
                               .Include(x => x.ProductBrand)
                               .Include(x => x.ProductType)
                               .FirstOrDefaultAsync(x => x.Id == productId);
    }

    public async Task<IReadOnlyList<Product>> GetProducts()
    {
        if (this.dbContext.Products == null) {
            return new List<Product>().AsReadOnly();
        }
        return (await this.dbContext.Products
                               .Include(x => x.ProductBrand)
                               .Include(x => x.ProductType)
                               .ToListAsync<Product>())
                               .AsReadOnly();
    }
}
