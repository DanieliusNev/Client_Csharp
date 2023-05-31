using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Shared.Models;

namespace Client_Blazor_App.Service.Http
{
    public class JwtAuthService : IAuthService
    {
        private UserState? _currentUser;
        private readonly HttpClient _httpClient;

        public JwtAuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
        
        public async Task LoginAsync(string username, string password)
        {
            UserState userState = await AuthenticateUserAsync(username, password);

            if (!userState.IsLoggedIn)
            {
                throw new Exception("Login failed. Invalid credentials.");
            }

            _currentUser = userState;

            ClaimsPrincipal principal = CreateClaimsPrincipal();

            OnAuthStateChanged.Invoke(principal);
        }
        
        public async Task RegisterUserAsync(string username, string password)
        {
             User user = new User(username, password);

                var json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://localhost:8080/register", content);
                string responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(responseContent);
                }
        }

        public async Task<ClaimsPrincipal> GetAuthAsync()
        {
            ClaimsPrincipal principal = CreateClaimsPrincipal();
            return principal;
        }

        public Task LogoutAsync()
        {
            _currentUser = null;

            ClaimsPrincipal principal = new();
            OnAuthStateChanged.Invoke(principal);
            return Task.CompletedTask;
        }

        private async Task<UserState> AuthenticateUserAsync(string username, string password)
        {
            var httpClient = new HttpClient();
            var request = new
            {
                Username = username,
                Password = password
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("http://localhost:8080/login", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the user state from the response
                var userState = JsonSerializer.Deserialize<UserState>(responseContent);
                return userState;
            }

            return new UserState(string.Empty, string.Empty);
        }

        private ClaimsPrincipal CreateClaimsPrincipal()
        {
            if (_currentUser == null || !_currentUser.IsLoggedIn)
            {
                return new ClaimsPrincipal();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _currentUser.Username),
                new Claim("UserId", _currentUser.Id.ToString()),
                new Claim("Password", _currentUser.Password)
            };

            var identity = new ClaimsIdentity(claims, "jwt");
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
    }
}
