using System.Data;
using System.Threading.Tasks;
using Dapper;
using Shareposts.Core.DataAccess;
using Shareposts.Core.Dtos.Db;
using Shareposts.Core.Dtos.UseCases;

namespace Shareposts.DataAccess;

public class UserDataAccess : IUserDataAccess
{
    private readonly IDbConnection connection;

    public UserDataAccess(IDbConnection connection)
    {
        this.connection = connection;
    }

    private UserDbDto MapUserRowToDto(dynamic userRow) => new UserDbDto() {
        id = userRow.id,
        name = userRow.name,
        email = userRow.email,
        phone = userRow.phone,
        passwordHash = userRow.password_hash,
        createdAt = userRow.created_at
    };

    public async Task<UserDbDto?> FindUserByEmail(string email)
    {
        var sql = "SELECT id, name, email, phone, password_hash FROM users WHERE email = @Email";
        var userRow = await this.connection.QueryFirstOrDefaultAsync(sql, new { @Email = email });
        if (userRow == null) {
            return null;
        }
        return MapUserRowToDto(userRow);
    }

    public async Task CreateUser(CreateUserDto newUser, string passwordHash)
    {
        var sql = "INSERT INTO users (name, email, phone, password_hash)" +
                  "VALUES (@Name, @Email, @Phone, @PasswordHash)";
        await this.connection.ExecuteAsync(sql, new {
            @Name = newUser.name,
            @Email = newUser.email,
            @Phone = newUser.phone,
            @PasswordHash = passwordHash
        });
    }
}
