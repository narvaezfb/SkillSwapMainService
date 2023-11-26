using System;
namespace SkillSwapMainService.Interfaces
{
	public interface ITokenValidationService
	{
        Task<bool> ValidateTokenAsync(string token);
    }
}

