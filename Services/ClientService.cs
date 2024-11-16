
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

        public async Task<bool> Signup(SignupModel model)
        {
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            var result = await httpClient.PostAsJsonAsync("/api/User/signup", model);
            if (result.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Alert", "Registration successful", "OK");
                return true;
            }

            return false;
        }

        public async Task<bool> Login(LoginModel model)
        {
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            try
            {
                // Send login request
                var result = await httpClient.PostAsJsonAsync("/api/User/login", model);

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

                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                // Handle any unexpected errors (e.g., network issues)
                await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }

            return false;
        }

        public async Task<List<ProductList>> GetProductListsAsync()
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null)
            {
                await Shell.Current.DisplayAlert("Error", "No authentication token found.", "OK");
                return null;
            }

            string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken;
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await httpClient.GetStringAsync("/api/Product/get");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var apiResponse = JsonSerializer.Deserialize<ProductListResponse>(response, options);

                if (apiResponse == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Failed to deserialize response.", "OK");
                    return new List<ProductList>();
                }

                if (apiResponse.Products != null && apiResponse.Products.Any())
                {
                    return apiResponse.Products;
                }

                return new List<ProductList>();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while fetching products: {ex.Message}", "OK");
                return new List<ProductList>();
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
