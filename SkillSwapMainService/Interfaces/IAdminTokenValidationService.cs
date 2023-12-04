using System;
namespace SkillSwapMainService.Interfaces
{
	public interface IAdminTokenValidationService
	{
        Task<bool> ValidateTokenAsync(string token);
    }
}

