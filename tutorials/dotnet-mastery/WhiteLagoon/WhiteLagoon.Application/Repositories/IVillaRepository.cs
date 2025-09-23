using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Application.Repositories;

public interface IVillaRepository
{
    IEnumerable<Villa> FindAll();
    Villa? FindById(int villaId);
    void Create(Villa villa);
    void Update(Villa villa);
    void Remove(int villaId);
    void Save();
}
