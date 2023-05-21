namespace BlazorWasm.Services;

public interface IExerciseService
{
    public Task<bool> RegisterExerciseAsync(string title, DateOnly date);
}