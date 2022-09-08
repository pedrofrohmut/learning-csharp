using System.Collections.Generic;
using CloudCustomers.Api.Entities;

namespace CloudCustomers.UnitTests.Fixtures;

public static class UsersFixtures
{
    public static List<User> GetListWith1User() => new List<User>() { 
        new User() { 
            Id = 1,
            Name = "John Doe",
            Email = "john@doe.com",
            Address = new Address() {
                Street = "123, Main St",
                City = "Center city",
                ZipCode = "12345"
            }
        } 
    };
    
    public static List<User> GetListWith3Users() => new List<User>() {
        new User() {
            Name = "User 1",
            Email = "user1@mail.com",
            Address = new Address() {
                Street = "123 First St",
                City = "First City",
                ZipCode = "12345-1"
            }
        },
        new User() {
            Name = "User 2",
            Email = "user2@mail.com",
            Address = new Address() {
                Street = "123 Second St",
                City = "Second City",
                ZipCode = "12345-2"
            }
        },
        new User() {
            Name = "User 3",
            Email = "user3@mail.com",
            Address = new Address() {
                Street = "123 Third St",
                City = "Third City",
                ZipCode = "12345-3"
            }
        },
    };
}
