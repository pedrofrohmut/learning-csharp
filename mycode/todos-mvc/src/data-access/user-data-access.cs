using System.Data;
using TodosMvc.Core.DataAccess;
using TodosMvc.Core.Dtos;
using Dapper;

namespace TodosMvc.DataAccess;

public class UserDataAccess : IUserDataAccess
{
    private readonly IDbConnection connection;

    public UserDataAccess(IDbConnection connection)
    {
        this.connection = connection;
    }

    public async Task CreateUser(CreateUserDto newUser, string passwordHash)
    {
        var sql = "INSERT INTO users (name, email, password_hash, phone) " +
                  "VALUES (@Name, @Email, @PasswordHash, @Phone)";
        await this.connection.ExecuteAsync(sql,
            new { Name = newUser.name, Email = newUser.email, PasswordHash = passwordHash, Phone = newUser.phone });
    }

    public async Task<UserDbDto?> FindUserByEmail(string email)
    {
        var sql = "SELECT id, name, email, password_hash, phone " +
                  "FROM users WHERE email = @Email";
        var userRow = await this.connection.QueryFirstOrDefaultAsync(sql, new { Email = email });

        if (userRow == null) return null;

        return new UserDbDto() {
            id = userRow.id,
            name = userRow.name,
            email = userRow.email,
            phone = userRow.phone,
            passwordHash = userRow.password_hash,
        };
    }

    public async Task<UserDbDto?> FindUserById(Guid userId)
    {
        var sql = "SELECT id, name, email, password_hash, phone " +
                  "FROM users WHERE id = @Id";
        var userRow = await this.connection.QueryFirstOrDefaultAsync(sql, new { Id = userId });

        if (userRow == null) return null;

        return new UserDbDto() {
            id = userRow.id,
            name = userRow.name,
            email = userRow.email,
            phone = userRow.phone,
            passwordHash = userRow.password_hash,
        };
    }
}
