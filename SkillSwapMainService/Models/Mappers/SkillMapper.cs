using System;
using SkillSwapMainService.Models.RequestModels;
namespace SkillSwapMainService.Models.Mappers
{
	public class SkillMapper
	{
		public Skill MapToSkillEntity(CreateSkillRequest createSkillRequest)
		{
			return new Skill
			{
				Name = createSkillRequest.Name,
				Description = createSkillRequest.Description,
				CategoryID = createSkillRequest.CategoryID,
				Level = createSkillRequest.Level,
				OwnerID = createSkillRequest.OwnerID
			};

		}
	}
}

