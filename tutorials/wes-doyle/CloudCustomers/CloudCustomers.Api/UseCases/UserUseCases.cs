using CloudCustomers.Api.Entities;

namespace CloudCustomers.Api.UseCases;

public class UserUseCases : IUserUseCases
{
    private readonly HttpClient httpClient;

    public UserUseCases(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
   
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }
}
