using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

using Shared.Models;

namespace Client_Blazor_App.Service.Http
{
    public class ExerciseService : IExerciseService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpContextAccessor _httpContextAccessor;

        public ExerciseService(HttpClient httpClient, HttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:8080");
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task RegisterExerciseAsync(string title, DateTime date, int userId)
        {
            ExerciseRegister exercise = new ExerciseRegister(title, date, userId);

                var json = JsonSerializer.Serialize(exercise);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("registerExercises", content);

                string responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(responseContent);
                }
        }
        
        public async Task<List<Exercise>> GetUserExercisesByDateAsync(int userId, DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            var response = await _httpClient.GetAsync($"exercisesDate/{userId}?startDate={startDateString}&endDate={endDateString}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
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

        public async Task<List<Exercise>> GetUserExercisesAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"exercises/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
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
        public async Task UpdateExerciseAsync(int id, string title, DateTime date, int userId)
        {
            ExerciseUpdate exercise = new ExerciseUpdate(id, title, date, userId);

            var json = JsonSerializer.Serialize(exercise);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"update", content);

            string responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }
        }
        public async Task DeleteExerciseAsync(int exerciseId)
        {
            var response = await _httpClient.DeleteAsync($"delete/{exerciseId}");

            string responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }
        }
        
        
    }
}
