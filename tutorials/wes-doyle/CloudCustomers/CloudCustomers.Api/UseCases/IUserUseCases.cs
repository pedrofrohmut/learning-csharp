using CloudCustomers.Api.Entities;

namespace CloudCustomers.Api.UseCases;

public interface IUserUseCases
{
    Task<IEnumerable<User>> GetAllUsers();
}
