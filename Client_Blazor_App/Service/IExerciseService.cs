using Shared.Models;

namespace Client_Blazor_App.Service;

public interface IExerciseService
{
    public Task RegisterExerciseAsync(string title, DateTime date, int userId);
    public Task<List<Exercise>> GetUserExercisesAsync(int userId);
    
}