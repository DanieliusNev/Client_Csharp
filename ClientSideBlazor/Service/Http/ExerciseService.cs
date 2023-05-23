using BlazorWasm.Services;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Shared.Models;


namespace ClientSideBlazor.Service.Http;

public class ExerciseService : IExerciseService
{
    private readonly HttpClient _httpClient;
    private HttpContextAccessor _httpContextAccessor;
    [Inject] protected UserState UserState { get; set; }

    public ExerciseService(HttpClient httpClient, HttpContextAccessor accessor)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:8080");
        _httpContextAccessor = accessor;
    }


    public async Task<bool> RegisterExerciseAsync(string title, DateTime date)
    {
        try
        {
            UserState userState = GetUserState();
            Exercise exercise = new Exercise(title,
                date,
                userState.Id);
            Console.WriteLine(userState.Id);
            Console.WriteLine(exercise.Date);

            var json = JsonSerializer.Serialize(exercise);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:8080/registerExercises", content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public async Task<List<Exercise>> GetUserExercisesAsync(long userId)
    {
        var response = await _httpClient.GetAsync($"http://localhost:8080/exercises/{userId}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            var exercises = JsonSerializer.Deserialize<List<Exercise>>(content);
            foreach (var exercise in exercises)
            {
                Console.WriteLine($"Exercise: Title={exercise.Title}, Date={exercise.Date}, UserId={exercise.UserId}");
            }
            return exercises;
        }
        else
        {
            return null;
        }
    }


    public UserState GetUserState()
    {
        // Retrieve the user state from the cookie
        var serializedUserState = _httpContextAccessor.HttpContext.Request.Cookies["UserState"];
        if (!string.IsNullOrEmpty(serializedUserState))
        {
            return JsonSerializer.Deserialize<UserState>(serializedUserState);
        }

        return null;

    }
}
