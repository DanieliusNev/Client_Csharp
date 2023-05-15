using System.Net.Http.Json;
using Shared.Models;

namespace BlazorWasm.Service;

public class PackageService
{
    private readonly HttpClient _httpClient;

    public PackageService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<PackageBox>> GetPackagesAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<IEnumerable<PackageBox>>("http://localhost:8080/report");
        return result;
    }
}
