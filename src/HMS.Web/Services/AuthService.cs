namespace HMS.Web.Services;

public class AuthService
{
    private readonly ApiService _api;
    public AuthService(ApiService api)
    {
        _api = api;
    }

    public async Task RegisterAsync(string username, string email, string password)
    {
        var model = new { Username = username, Email = email, Password = password };
        await _api.PostAsync("api/auth/register", model);
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        var model = new { Username = username, Password = password };
        var response = await _api.PostAsync<object, LoginResponse>("api/auth/login", model);
        return response?.Token ?? "";
    }
}

public class LoginResponse
{
    public string Token { get; set; } = "";
}