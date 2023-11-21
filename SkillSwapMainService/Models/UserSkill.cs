using System;
namespace SkillSwapMainService.Models
{
	public class UserSkill
	{
        public int UserSkillID { get; set; }

        public required int UserID { get; set; }

        public required int SkillID { get; set; }
    
        public required DateTime DateAdded { get; set; }

        public required Skill Skill{ get; set; }
    
    }
}

