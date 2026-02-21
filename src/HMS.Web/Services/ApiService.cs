using System.Net.Http.Json;

namespace HMS.Web.Services;

public class ApiService
{
    private readonly HttpClient _http;
    public ApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        var response = await _http.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data)
    {
        var response = await _http.PostAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task PostAsync<TRequest>(string url, TRequest data)
    {
        var response = await _http.PostAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
    }
}