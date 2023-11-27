using System;
namespace SkillSwapMainService.Models.RequestModels.UserSkills
{
	public class CreateUserSkillRequest
	{
		public int UserId { get; set; }
		public int SkillId { get; set; }
	}
}

