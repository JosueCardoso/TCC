using Estimatz.UI.ExternalServices.EstimatzLoginApi.Common;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Login;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.RecoverPassword;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Register;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Estimatz.UI.ExternalServices.EstimatzLoginApi
{
    public class LoginApiClient : ILoginApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;

        public LoginApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("LoginApi");
        }

        public async Task<CommonResponse> Register(RegisterRequest request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/Account/register", content);
            var responseString = await response.Content?.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ? new CommonResponse() : JsonConvert.DeserializeObject<CommonResponse>(responseString);
        }

        public async Task<CommonResponse> ConfirmEmail(string userId, string token)
        {
            var response = await _client.GetAsync($"/api/v1/Account/confirm-email?userId={userId}&token={WebUtility.UrlEncode(token)}");
            var responseString = await response.Content.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ? new CommonResponse() : JsonConvert.DeserializeObject<CommonResponse>(responseString);
        }

        public async Task<CommonResponse> RecoverPassword(RecoverPasswordRequest request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/v1/Account/recover-password", content);
            var responseString = await response.Content.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ? new CommonResponse() : JsonConvert.DeserializeObject<CommonResponse>(responseString);
        }

        public async Task<CommonResponse> ConfirmRecoverPassword(ConfirmRecoverPasswordRequest request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/v1/Account/confirm-recover-password", content);
            var responseString = await response.Content.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ? new CommonResponse() : JsonConvert.DeserializeObject<CommonResponse>(responseString);
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/v1/Account/login", content);
            var responseString = await response.Content.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ? new LoginResponse() : JsonConvert.DeserializeObject<LoginResponse>(responseString);
        }

        public async Task<CommonResponse> ValidateUser(string userId, string token)
        {
            var response = await _client.GetAsync($"/api/v1/Token/validate-user?userId={userId}&token={WebUtility.UrlEncode(token)}");
            var responseString = await response.Content.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ? new CommonResponse() : JsonConvert.DeserializeObject<CommonResponse>(responseString);
        }
    }
}
