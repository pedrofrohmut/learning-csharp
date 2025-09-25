using WhiteLagoon.Application.Repositories;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repositories;

public class VillaRepository : IVillaRepository
{
    private readonly ApplicationDbContext dbContext;

    public VillaRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IEnumerable<Villa> FindAll()
    {
        return this.dbContext.Villas.ToList();
    }

    public Villa? FindById(int villaId)
    {
        return this.dbContext.Villas.FirstOrDefault(x => x.Id == villaId);
    }

    public void Create(Villa villa)
    {
        this.dbContext.Add(villa);
    }

    public void Update(Villa villa)
    {
        this.dbContext.Update(villa);
    }

    public void Remove(int villaId)
    {
        this.dbContext.Remove(new Villa { Id = villaId, Name = "" });
    }

    public void Save()
    {
        this.dbContext.SaveChanges();
    }
}
