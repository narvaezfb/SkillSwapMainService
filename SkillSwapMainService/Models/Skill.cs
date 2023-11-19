using System.ComponentModel.DataAnnotations;
namespace SkillSwapMainService.Models
{
	public class Skill
	{
        public int SkillID { get; set; }

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

        public DateTime DateAdded { get; set; }

        public DateTime LastModified { get; set; }

        public Boolean IsVerified { get; set; }

       

    }
}

