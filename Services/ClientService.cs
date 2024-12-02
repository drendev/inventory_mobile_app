
using inventory_mobile_app.Models;
using inventory_mobile_app.Pages.Auth;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ZXing.QrCode.Internal;

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
                                UserName = response.UserName
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

        public async Task<bool> AddProductAsync(Product product)
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null)
            {
                await Shell.Current.DisplayAlert("Error", "No authentication token found.", "OK");
            }

            string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken;
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await httpClient.PostAsJsonAsync("/api/Product/add", product);

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while adding product: {ex.Message}", "OK");
                return false;
            }
        }

        public async Task<List<ProductList>> GetProductListsAsync()
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null)
            {
                await Shell.Current.DisplayAlert("Error", "No authentication token found.", "OK");
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

        public async Task<Product> GetProductAsync(string barcode)
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
                var response = await httpClient.GetStringAsync($"/api/Product/get/{barcode}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var apiResponse = JsonSerializer.Deserialize<ProductResponse>(response, options);

                if (apiResponse.Product == null)
                {
                    return null;
                }


                return apiResponse.Product;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while fetching products: {ex.Message}", "OK");
                return new Product();
            }
        }

        public async Task<bool> AddStock(StockModel model)
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null)
            {
                await Shell.Current.DisplayAlert("Error", "No authentication token found.", "OK");
                return false;
            }

            string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken;
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await httpClient.PostAsJsonAsync("/api/Product/addstock", model);

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while adding stock: {ex.Message}", "OK");
                return false;
            }
        }

        // update product

        public async Task<bool> UpdateProductAsync(UpdateProduct updateProduct)
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null)
            {
                await Shell.Current.DisplayAlert("Error", "No authentication token found.", "OK");
                return false;
            }

            string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken;
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await httpClient.PutAsJsonAsync("/api/Product/update", updateProduct);

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while adding stock: {ex.Message}", "OK");
                return false;
            }
        }

        public async Task<bool> SoldStock(StockModel model)
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null)
            {
                await Shell.Current.DisplayAlert("Error", "No authentication token found.", "OK");
                return false;
            }

            string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken;
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await httpClient.PostAsJsonAsync("/api/Product/removestock", model);

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while adding stock: {ex.Message}", "OK");
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(string productId)
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null)
            {
                await Shell.Current.DisplayAlert("Error", "No authentication token found.", "OK");
                return false;
            }

            string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken;
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await httpClient.DeleteAsync($"/api/Product/delete?productId={productId}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while adding stock: {ex.Message}", "OK");
                return false;
            }
        }

        // Report List
        public async Task<List<Report>> GetReportListAsync()
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null)
            {
                await Shell.Current.DisplayAlert("Error", "No authentication token found.", "OK");
            }

            string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken;
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await httpClient.GetStringAsync("/api/Report/list");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var apiResponse = JsonSerializer.Deserialize<ReportList>(response, options);

                if (apiResponse == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Failed to deserialize response.", "OK");
                    return new List<Report>();
                }

                if (apiResponse.Reports != null && apiResponse.Reports.Any())
                {
                    return apiResponse.Reports;
                }

                return new List<Report>();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while fetching products: {ex.Message}", "OK");
                return new List<Report>();
            }
        }

        // Record report

        public async Task<bool> RecordReport(RecordReportModel model)
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null)
            {
                await Shell.Current.DisplayAlert("Error", "No authentication token found.", "OK");
                return false;
            }

            string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken;
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await httpClient.PostAsJsonAsync("/api/Report/add", model);

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while adding stock: {ex.Message}", "OK");
                return false;
            }
        }

        // Dashboard

        public async Task<Dashboard> GetDashboardAsync()
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
                var response = await httpClient.GetStringAsync($"/api/Dashboard/get");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var apiResponse = JsonSerializer.Deserialize<DashboardList>(response, options);

                if (apiResponse.Dashboard == null)
                {
                    return null;
                }


                return apiResponse.Dashboard;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while fetching products: {ex.Message}", "OK");
                return new Dashboard();
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
