using System.Collections.Generic;
using System.Threading.Tasks;
using Skinet.Core.Entities;

namespace Skinet.Core.Repositories;

public interface IProductBrandRepository
{
    Task<IReadOnlyList<ProductBrand>> GetProductBrands();
}
