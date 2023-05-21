using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace BlazorWasm.Service;

public class DemoService
{
    private readonly HttpClient _httpClient;

    public DemoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        /*_httpClient.BaseAddress = new Uri("http://localhost:8080/");*/
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> GetHelloMessageAsync(string accessToken)
    {
        
        /*// Set the authorization header with the access token
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Make a GET request to the "/api/v1/demo-controller" endpoint
        HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/demo-controller");

        if (response.IsSuccessStatusCode)
        {
            // Read the response content
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }

        throw new Exception("Failed to retrieve the hello message.");*/
        

        /*// Set the 'Access-Control-Allow-Origin' header
        _httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");*/
        // Set the authorization header with the access token
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Create the request message
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8080/api/v1/demo-controller");

        

        // Send the request
        HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

        if (response.IsSuccessStatusCode)
        {
            // Read the response content
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }

        throw new Exception("Failed to retrieve the hello message.");
        
    }
}