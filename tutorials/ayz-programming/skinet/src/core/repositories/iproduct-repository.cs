using System.Collections.Generic;
using System.Threading.Tasks;
using Skinet.Core.Entities;

namespace Skinet.Core.Repositories;

public interface IProductRepository
{
    Task<Product?> GetProductById(int productId);
    Task<IReadOnlyList<Product>> GetProducts();
}
