
using System.Text.Json.Serialization;

namespace Shared.Models;

public class Exercise
{
    [JsonIgnore]
    public int Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    public int UserId { get; set; }

    public Exercise(string title, DateTime date, int idUser)
    {
        
        Title = title;
        Date = date.Date; // Set only the date component, without the time
        UserId = idUser;
    }


    public Exercise()
    {
        
    }
    
}