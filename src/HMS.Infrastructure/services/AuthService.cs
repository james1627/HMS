namespace HMS.Infrastructure.Services;

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

    public async Task<string?> LoginAsync(string email, string password)
    {
        var model = new { Email = email, Password = password };
        try
        {
            var response = await _api.PostAsync<object, LoginResponse>("api/auth/login", model);
            if (string.IsNullOrEmpty(response?.Token))
            {
                throw new Exception("Login failed: Invalid token returned.");
            }

            _api.SetBearerTokenAsync(response.Token);

            return response.Token;
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Unauthorized"))
            {
                return null;
            }
            // Log the exception or handle it as needed
            throw new Exception($"Login failed: {ex.Message}");
        }
    }

    public async Task LoginWithTokenAsync(string token)
    {
        // Optionally, you could validate the token here before setting it
        _api.SetBearerTokenAsync(token);
    }

    public void Logout()
    {
        _api.SetBearerTokenAsync(string.Empty);
    }
}

public class LoginResponse
{
    public string Token { get; set; } = "";
}