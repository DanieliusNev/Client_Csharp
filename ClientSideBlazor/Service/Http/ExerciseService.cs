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


    public async Task<bool> RegisterExerciseAsync(string title, DateOnly date)
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
