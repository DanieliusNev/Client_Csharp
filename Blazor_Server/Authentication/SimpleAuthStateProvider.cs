using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blazor_Server.Authentication;

public class SimpleAuthStateProvider : AuthenticationStateProvider
{
    private readonly IAuthManager authManager;

    public SimpleAuthStateProvider(IAuthManager authManager)
    {
        this.authManager = authManager;
        authManager.OnAuthStateChanged += AuthStateChanged;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await authManager.GetAuthAsync();
        return await Task.FromResult(new AuthenticationState(principal));
    }

    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
}