using System.Security.Claims;

namespace Client_Blazor_App.Service;

public interface IAuthService
{
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task RegisterUserAsync(string username, string password);
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    /*UserState GetUserState();*/
    public Task<ClaimsPrincipal> GetAuthAsync();
}