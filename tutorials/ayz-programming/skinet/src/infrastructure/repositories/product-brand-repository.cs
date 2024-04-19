using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Repositories;
using Skinet.Infrastructure.Data;

namespace Skinet.Infrastructure.Repositories;

public class ProductBrandRepository : IProductBrandRepository
{
    private readonly StoreDbContext dbContext;

    public ProductBrandRepository(StoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrands()
    {
        if (this.dbContext.ProductBrands == null) {
            return new List<ProductBrand>().AsReadOnly();
        }
        return (await this.dbContext.ProductBrands.ToListAsync<ProductBrand>()).AsReadOnly();
    }
}
