using Shared.Models;

namespace Client_Blazor_App.Service;

public interface IShareService
{
    public Task ShareExercisesAsync(string userId, List<ExerciseRegister> exercises, string comment);

    public Task<List<SharedPost>> GetSharedPostsByUserAsync();
}