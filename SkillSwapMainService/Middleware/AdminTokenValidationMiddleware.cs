using Microsoft.AspNetCore.Http;
using SkillSwapMainService.Interfaces;
using System;
using System.Threading.Tasks;

namespace SkillSwapMainService.Middleware
{
    public class AdminTokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAdminTokenValidationService _adminTokenValidationService;

        public AdminTokenValidationMiddleware(RequestDelegate next, IAdminTokenValidationService adminTokenValidationService)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _adminTokenValidationService = adminTokenValidationService ?? throw new ArgumentNullException(nameof(adminTokenValidationService));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Extract the token from the Authorization header
                string? authorizationHeader = context.Request.Headers["Authorization"];
                string? token = authorizationHeader?.Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token is missing or invalid");
                    return;
                }

                // Use the TokenValidationService to validate the token
                bool isTokenValid = await _adminTokenValidationService.ValidateTokenAsync(token);

                if (isTokenValid)
                {
                    // Token is valid, proceed to the next middleware in the pipeline
                    await _next(context);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token is invalid");
                    return;
                }
            }
            catch (Exception e)
            {
                // Log the exception for debugging purposes
                // Consider logging more details and handle/logging specific exception types
                Console.WriteLine($"Exception occurred: {e}");

                // Respond with a generic error message
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Internal server error");
            }

        }
    }
}
