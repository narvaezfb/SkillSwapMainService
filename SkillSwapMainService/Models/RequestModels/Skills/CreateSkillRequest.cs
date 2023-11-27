using System;
using System.ComponentModel.DataAnnotations;

namespace SkillSwapMainService.Models.RequestModels
{
	public class CreateSkillRequest
	{
        [Required(ErrorMessage = "Skill name is required")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Skill description is required")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Skill category is required")]
        public required string Category { get; set; }

        [Required(ErrorMessage = "Skill level is required")]
        public required string Level { get; set; }

        [Required(ErrorMessage = "Skill Owner ID is required")]
        public required int OwnerID { get; set; }
    }
}

