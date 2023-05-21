using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using BlazorWasm.Services;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;


namespace ClientSideBlazor.Service.Http;

public class JwtAuthService: IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly UserState _userState;
    private HttpContextAccessor _httpContextAccessor;
    [Inject]
    protected UserState UserState { get; set; }
    public static string? Jwt { get; private set; } = "";

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;

    public JwtAuthService(HttpClient httpClient, UserState userState, HttpContextAccessor accessor)
    {
        _httpClient = httpClient;
        _userState = userState;
        _httpContextAccessor = accessor;
    }
    
    public async Task<bool> RegisterUserAsync(string username, string password)
    {
        try
        {
            User user = new User
            {
                Username = username,
                Password = password
            };

            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:8080/register", content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public async Task LoginUserAsync(string username, string password)
    {
        User user = new User
        {
            Username = username,
            Password = password
        };

        string json = JsonSerializer.Serialize(user);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:8080/login", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            // Deserialize the response content into a UserDto object
            UserDto? authenticatedUser = JsonSerializer.Deserialize<UserDto>(responseContent);
            Console.WriteLine(authenticatedUser.Username);
            _userState.Id = authenticatedUser.Id;
            _userState.Username = authenticatedUser.Username;
            Console.WriteLine(_userState.Username);
            _userState.Password = authenticatedUser.Password;
            
            var options = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(1) // Set the cookie expiration time
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("UserState", JsonSerializer.Serialize(authenticatedUser), options);
        }
        else
        {
            throw new Exception("Authentication failed");
        }
        
    }
    public UserState GetUserState()
    {
        // Retrieve the user state from the cookie
        var serializedUserState = _httpContextAccessor.HttpContext.Request.Cookies["UserState"];
        if (!string.IsNullOrEmpty(serializedUserState))
        {
            return JsonSerializer.Deserialize<UserState>(serializedUserState);
        }
        return null;
    }


    public Task LogoutAsync()
    {
        Jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
        return Task.CompletedTask;
    }
    
    
}