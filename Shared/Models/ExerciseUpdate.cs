using System.Text.Json.Serialization;

namespace Shared.Models;

public class ExerciseUpdate
{
    [JsonPropertyName("id")]
        public int Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    

    public ExerciseUpdate(int id, string title, DateTime date, int userId)
    {
        Id = id;
        Title = title;
        Date = date;
        UserId = userId;
    }


    public ExerciseUpdate()
    {
        
    }
    
}