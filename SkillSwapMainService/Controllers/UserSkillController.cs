using Microsoft.AspNetCore.Mvc;
using SkillSwapMainService.Models;
using SkillSwapMainService.Models.RequestModels.UserSkills;
using SkillSwapMainService.Models.Mappers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;

namespace SkillSwapMainService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserSkillController : ControllerBase
	{
        private readonly SkillSwapDbContext _context;

        public UserSkillController(SkillSwapDbContext context)
		{
            _context = context;
        }

        [HttpPost("CreateUserSkill", Name = "CreateUserSkill")]
        public async Task<ActionResult> CreateUserSkill([FromBody] CreateUserSkillRequest createUserSkillRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Request Data");
                }

                UserSkill userSkill = new UserSkill
                {
                    UserID = createUserSkillRequest.UserId,
                    SkillID = createUserSkillRequest.SkillId
                };

                _context.UserSkill.Add(userSkill);
                await _context.SaveChangesAsync();

                return Ok(userSkill);
            }
            catch (DbUpdateException dbUpdateException)
            {
                return BadRequest("Database error occurred: " + dbUpdateException.InnerException?.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An error occurred while processing the request: " + e.Message);
            }

        }
        [HttpGet("GetUserSkill/{userSkillId}", Name = "GetUserSkill")]
        public async Task<ActionResult> GetUserSkill(int userSkillId)
        {
            try
            {
                var userSkill = await _context.UserSkill.FindAsync(userSkillId);

                if (userSkill == null)
                {
                    return BadRequest("No userSkill found with that ID");
                }

                var skill = await _context.Skill.FindAsync(userSkill.SkillID);

                if (skill != null)
                {
                    userSkill.Skill = skill;
                }
                return Ok(userSkill);
            }
            catch (DbUpdateException dbUpdateException)
            {
                return BadRequest("Database error occurred: " + dbUpdateException.InnerException?.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An error occurred while processing the request: " + e.Message);
            }
        }
        [HttpDelete("DeleteUserSkill/{userSkillId}", Name ="DeleteUserSkill")]
        public async Task<ActionResult> DeleteUserSkill(int userSkillId)
        {
            try
            {
                var userSkill = await _context.UserSkill.FindAsync(userSkillId);

                if(userSkill == null)
                {
                    return BadRequest("No userSkill found with that ID");
                }

                _context.UserSkill.Remove(userSkill);
                await _context.SaveChangesAsync();

                return Ok("UserSkill has been deleted");
            }
            catch (DbUpdateException dbUpdateException)
            {
                return BadRequest("Database error occurred: " + dbUpdateException.InnerException?.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An error occurred while processing the request: " + e.Message);
            }
           
        }

    }
}

