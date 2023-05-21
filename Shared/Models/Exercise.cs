
using System.Text.Json.Serialization;

namespace Shared.Models;

public class Exercise
{
    [JsonIgnore]
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public DateOnly Date { get; set; }
    
    public int UserId { get; set; }

    public Exercise(string title, DateOnly date, int idUser)
    {
        Title = title;
        Date = date;
        UserId = idUser;
    }

    public Exercise()
    {
        
    }
    
}