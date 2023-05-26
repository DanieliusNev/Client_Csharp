using System.Text.Json.Serialization;

namespace Shared.Models;

public class UserState
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }
    [JsonPropertyName("Username")]
    public string Username { get; set; }
    [JsonPropertyName("Password")]
    public string Password { get; set; }
    public bool IsLoggedIn => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
    
    public UserState(string username, string password)
    {
        Username = username;
        Password = password;
    }

}