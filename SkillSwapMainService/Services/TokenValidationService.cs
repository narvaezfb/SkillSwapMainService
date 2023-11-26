using System.Net;
using System.Text;
using SkillSwapMainService.Interfaces;

namespace SkillSwapMainService.Services
{
    public class TokenValidationService : ITokenValidationService
    {
        private readonly HttpClient _httpClient;
        private const string AuthApiBaseUrl = "https://localhost:7214"; 

        public TokenValidationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(AuthApiBaseUrl);
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            var body = new StringContent($"\"{token}\"", Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"Auth/validateToken", body);

            if (response.IsSuccessStatusCode)
            {
                // Token validation successful
                return true;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Token validation failed (token is invalid or expired)
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

