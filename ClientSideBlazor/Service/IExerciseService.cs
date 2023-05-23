using Shared.Models;

namespace BlazorWasm.Services;

public interface IExerciseService
{
    public Task<bool> RegisterExerciseAsync(string title, DateTime date);
    public Task<List<Exercise>> GetUserExercisesAsync(long userId);
    public UserState GetUserState();
}