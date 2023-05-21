using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Shared.Models;

public class UserDto
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }
}