using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SkillSwapMainService.Models
{
	public class Category
	{
		public int CategoryID { get; set; }

		[Required(ErrorMessage ="Category must have a name")]
		public string Name { get; set; }

		[Required(ErrorMessage="Category must have a description")]
		public string Description { get; set; }

        [Required(ErrorMessage = "Category must have a creation date")]
        public DateTime DateCreated { get; set; }

        public DateTime LastModified { get; set; }

        [JsonIgnore]
        public ICollection<Skill> Skills { get; set; }
    }
}

