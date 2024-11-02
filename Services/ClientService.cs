
using inventory_mobile_app.Models;
using inventory_mobile_app.Pages.Auth;
using System.Net.Http.Json;
using System.Text.Json;

namespace inventory_mobile_app.Services
{
    public class ClientService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ClientService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task Register(RegisterModel model)
        {
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            var result = await httpClient.PostAsJsonAsync("/register", model);
            if (result.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Alert", "Registration successful", "OK");
            }

            await Shell.Current.DisplayAlert("Alert", result.ReasonPhrase, "Ok");
            await Shell.Current.GoToAsync(nameof(SignupPage));
        }

        public async Task Login(LoginModel model)
        {
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            try
            {
                // Send login request
                var result = await httpClient.PostAsJsonAsync("/login", model);

                if (result.IsSuccessStatusCode)
                {
                    // Deserialize the response if successful
                    var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
                    if (response is not null)
                    {
                        // Store authentication details securely
                        var serializeResponse = JsonSerializer.Serialize(
                            new LoginResponse
                            {
                                AccessToken = response.AccessToken,
                                RefreshToken = response.RefreshToken,
                                UserName = model.Email
                            });

                        await SecureStorage.Default.SetAsync("Authentication", serializeResponse);

                        // Navigate to Category page upon successful login
                        await Shell.Current.GoToAsync(nameof(Category));
                    }
                }

            }
            catch (Exception ex)
            {
                // Handle any unexpected errors (e.g., network issues)
                await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        public async Task<Category[]> GetCategoriesData()
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null) return null;

            string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken;
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetFromJsonAsync<Category[]>("/categories");
            return result;
        }
    }
}
