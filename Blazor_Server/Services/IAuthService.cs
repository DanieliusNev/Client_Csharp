using System.Security.Claims;
using Shared.Models;

namespace Blazor_Server.Services;

public interface IAuthService
{
    public Task LoginUserAsync(string username, string password);
    public Task LogoutAsync();
    public Task<bool> RegisterUserAsync(string username, string password);
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    UserState GetUserState();
}