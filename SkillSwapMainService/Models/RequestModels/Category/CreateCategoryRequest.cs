using System;
using System.ComponentModel.DataAnnotations;

namespace SkillSwapMainService.Models.RequestModels.Category
{
	public class CreateCategoryRequest
	{
        [Required(ErrorMessage = "Category must have a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category must have a description")]
        public string Description { get; set; }
    }
}

