using Shared.Models;

namespace Blazor_Server.Services.Impls;

public class InMemoryUserService : IUserService
{
    public async Task<User?> GetUserAsync(string username)
    {
        User? find = users.Find(user => user.Username.Equals(username));
        return find;
    }

    private List<User> users = new()
    {
        new User("Troels", "Troels1234"),
        new User("Maria", "oneTwo3FOUR"),
        new User("Anne", "password")        
    };
}