using System.Collections.Generic;
using System.Threading.Tasks;
using Skinet.Core.Entities;

namespace Skinet.Core.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetById(int id);
    Task<IReadOnlyList<T>> GetAll();
}
