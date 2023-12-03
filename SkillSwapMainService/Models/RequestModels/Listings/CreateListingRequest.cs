using System;
namespace SkillSwapMainService.Models.RequestModels.UserSkills
{
	public class CreateLisitingRequest
	{
		public int UserId { get; set; }
		public int SkillId { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
		public string Availability { get; set; }
	}
}

