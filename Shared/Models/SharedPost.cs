using System.Text.Json.Serialization;

namespace Shared.Models;

public class SharedPost
{
    [JsonPropertyName("postId")]
    public int PostId { get; set; }
        
    [JsonPropertyName("sharedDate")]
    public string SharedDate { get; set; }
        
    [JsonPropertyName("comment")]
    public string Comment { get; set; }
        
    [JsonPropertyName("userId")]
    public int SharedBy { get; set; }
        
    [JsonPropertyName("exercises")]
    public List<SharedExercise> Exercises { get; set; }

    public SharedPost(string comment, int sharedBy, List<Exercise> exercises)
    {
        Comment = comment;
        SharedBy = sharedBy;
        Exercises = exercises.Select(e => new SharedExercise { ExerciseId = e.Id, Title = e.Title }).ToList();
    }
}

