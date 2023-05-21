namespace ClientSideBlazor.Service;

using System.Net.Http.Headers;

public class Demo
{
    private readonly HttpClient _httpClient;

    public Demo(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetHelloMessageAsync(string accessToken)
    {
        
        // Set the authorization header with the access token
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        _httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
        // Make a GET request to the "/api/v1/demo-controller" endpoint
        HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:8080/api/v1/demo-controller");

        if (response.IsSuccessStatusCode)
        {
            // Read the response content
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }

        throw new Exception("Failed to retrieve the hello message.");
        

    }
}