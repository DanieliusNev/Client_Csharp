using Shared.Models;

namespace Client_Blazor_App.Service;

public interface IShareService
{
    public Task ShareExercisesAsync(string userId, List<Exercise> exercises, string comment);

    public Task<List<SharedPost>> GetSharedPostsByUserAsync();
}