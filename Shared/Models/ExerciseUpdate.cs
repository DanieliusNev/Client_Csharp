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
    [JsonPropertyName("weights")]
    public string Weights { get; set; }
    [JsonPropertyName("amount")]
    public string Amount { get; set; }
    [JsonPropertyName("categoryId")]
    public int CategoryId { get; set; }
    public ExerciseUpdate(int id, string title, DateTime date, string weights, string amount, int categoryId, int userId)
    {
        Id = id;
        Title = title;
        Date = date;
        Weights = weights;
        Amount = amount;
        CategoryId = categoryId;
        UserId = userId;
    }


    public ExerciseUpdate()
    {
        
    }
    
}