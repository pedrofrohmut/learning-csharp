using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Repositories;
using Skinet.Infrastructure.Data;

namespace Skinet.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreDbContext dbContext;

    public GenericRepository(StoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IReadOnlyList<T>> GetAll()
    {
        return (await this.dbContext.Set<T>().ToListAsync()).AsReadOnly();
    }

    public async Task<T?> GetById(int id)
    {
        return await this.dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }
}
