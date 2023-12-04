using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkillSwapMainService.Interfaces;

namespace SkillSwapMainService.Services
{
    public class AdminTokenValidationService : IAdminTokenValidationService
    {
        private readonly HttpClient _httpClient;
        private const string AuthApiBaseUrl = "https://localhost:7214";

        public AdminTokenValidationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(AuthApiBaseUrl);
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            var body = new StringContent($"\"{token}\"", Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"Auth/validateAdminToken", body);


            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                JObject responseData = JObject.Parse(responseContent);

                bool isValidToken = (bool)responseData["isValidToken"];

                if (isValidToken)
                {
                    // Token validation successful
                    return true;
                }
                return false;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return false;
            }
            else
            {
                // Handle other status codes or errors accordingly
                throw new Exception("Token validation request failed");
            }
        }
    }

}

