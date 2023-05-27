using System.Text.Json.Serialization;

namespace Shared.Models;

public class ExerciseRegister
{
    [JsonIgnore]
    public int Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    

    public ExerciseRegister(string title, DateTime date, int userId)
    {
        Title = title;
        Date = date;
        UserId = userId;
    }


    public ExerciseRegister()
    {
        
    }
}