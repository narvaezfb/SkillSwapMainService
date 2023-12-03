using System;
using System.ComponentModel.DataAnnotations;

namespace SkillSwapMainService.Models.RequestModels
{
	public class CreateSkillRequest
	{
        [Required(ErrorMessage = "Skill name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Skill description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Skill category is required")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Skill level is required")]
        public string Level { get; set; }

        [Required(ErrorMessage = "Skill Owner ID is required")]
        public int OwnerID { get; set; }
    }
}

