using Shared.Models;

namespace Blazor_Server.Services;

public interface IUserService
{
    public Task<User> GetUserAsync(string username);
}