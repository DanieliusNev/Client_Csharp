using System.Text.Json.Serialization;

namespace Shared.Models;

public class SharedExercise
{
    [JsonPropertyName("exerciseId")]
    public int ExerciseId { get; set; }
    
    [JsonPropertyName("exerciseTitle")]
    public string Title { get; set; }
}