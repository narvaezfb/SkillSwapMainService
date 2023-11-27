using System;
using System.ComponentModel.DataAnnotations;

namespace SkillSwapMainService.Models.RequestModels
{
	public class SkillUpdateRequest
	{
        
        public  string? Name { get; set; }

        public  string? Description { get; set; }

        public  string? Category { get; set; }

        public  string? Level { get; set; }

        public Boolean IsVerified { get; set; }
    }
}

