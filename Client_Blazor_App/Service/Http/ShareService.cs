using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Shared.Models;

namespace Client_Blazor_App.Service.Http
{
    public class ShareService : IShareService
    {
        private readonly HttpClient _httpClient;

        public ShareService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:8080");
        }

        public async Task ShareExercisesAsync(string userId, List<Exercise> exercises, string comment)
        {
            SharedPost shareData = new SharedPost(comment, userId, exercises);

            var json = JsonSerializer.Serialize(shareData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("exercises/share", content);

            string responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }
        }
        

        public async Task<List<SharedPost>> GetSharedPostsByUserAsync()
        {
            var response = await _httpClient.GetAsync("exercises/sharedPosts");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var sharedPosts = JsonSerializer.Deserialize<List<SharedPost>>(content);
                foreach (var sharedPost in sharedPosts)
                {
                    Console.WriteLine($"Shared Post: Id={sharedPost.PostId}, Date={sharedPost.SharedDate}, Comment={sharedPost.Comment}, SharedBy={sharedPost.SharedBy}");
                    foreach (var exercise in sharedPost.ExerciseTitles)
                    {
                        Console.WriteLine($"Shared Exercise: Title={exercise.Title}");
                    }
                }
                return sharedPosts;
            }
            else
            {
                return null;
            }
        }
    }
}