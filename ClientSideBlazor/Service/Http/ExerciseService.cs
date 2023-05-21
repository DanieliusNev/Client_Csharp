using BlazorWasm.Services;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Shared.Models;


namespace ClientSideBlazor.Service.Http;

public class ExerciseService : IExerciseService
{
    private readonly HttpClient _httpClient;
    [Inject]
    protected UserState UserState { get; set; }

    public ExerciseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<bool> RegisterExerciseAsync(string title, DateOnly date)
    {
        try
        {
            Exercise exercise = new Exercise(title,
                date,
                UserState.Id);
            Console.WriteLine(exercise.IdUser);
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
}