using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Repositories;
using Skinet.Infrastructure.Data;

namespace Skinet.Infrastructure.Repositories;

public class ProductTypeRepository : IProductTypeRepository
{
    private readonly StoreDbContext dbContext;

    public ProductTypeRepository(StoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypes()
    {
        if (this.dbContext.ProductTypes == null) {
            return new List<ProductType>().AsReadOnly();
        }
        return (await this.dbContext.ProductTypes.ToListAsync<ProductType>()).AsReadOnly();
    }
}
