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
        
    [JsonPropertyName("sharedBy")]
    public string SharedBy { get; set; }
        
    [JsonPropertyName("exerciseTitles")]
    public List<SharedExercise> ExerciseTitles { get; set; }

    public SharedPost() 
    {
        ExerciseTitles = new List<SharedExercise>();
    }

    public SharedPost(string comment, string sharedBy, List<ExerciseRegister> exercises)
    {
        Comment = comment;
        SharedBy = sharedBy;
        ExerciseTitles = exercises.Select(e => new SharedExercise { ExerciseId = e.Id, Title = e.Title }).ToList();
    }
}

