using Microsoft.AspNetCore.Mvc;
using SkillSwapMainService.Models;
using SkillSwapMainService.Models.RequestModels;
using SkillSwapMainService.Models.Mappers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace SkillSwapMainService.Controllers
{
    [ApiController]
    [Route("Admin/[Controller]")]
    public class SkillController: ControllerBase
	{
        private readonly SkillSwapDbContext _context;
        private readonly SkillMapper _skillMapper;


        public SkillController(SkillSwapDbContext context)
        {
            _context = context;
            _skillMapper = new SkillMapper();
        }

        [HttpPost("CreateSkill", Name ="CreateSkill")]
        public async Task<ActionResult> CreateSkill([FromBody] CreateSkillRequest createSkillRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Skill skill = _skillMapper.MapToSkillEntity(createSkillRequest);

                _context.Skills.Add(skill);
                await _context.SaveChangesAsync();

                return Ok(skill);
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

        [HttpGet("GetSkill/{skillID}", Name ="GetSkill")]
        public async Task<ActionResult> GetSkill(int skillID)
        {
            try
            {
                var skill = await _context.Skills.FindAsync(skillID);

                if (skill == null)
                {
                    return BadRequest("Skill not found with that ID");
                }

                return Ok(skill);
            }
            catch(Exception e)
            {
                return StatusCode(500, "An error occurred while processing the request: " + e.Message);
            }
           
        }

        [HttpPatch("UpdateSkill/{skillID}", Name = "UpdateSkill")]
        public async Task<ActionResult> UpdateSkill(int skillID, [FromBody] JsonPatchDocument<SkillUpdateRequest> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    return BadRequest("Not model included in payload");
                }

                var skill = await _context.Skills.FindAsync(skillID);

                if (skill == null)
                {
                    return BadRequest("Not skill found with that ID");
                }

                var skillToPatch = new SkillUpdateRequest
                {
                    Name = skill.Name,
                    Description = skill.Description,
                    CategoryID = skill.CategoryID,
                    Level = skill.Level,
                    IsVerified = skill.IsVerified
                };

                patchDocument.ApplyTo(skillToPatch, ModelState);


                if (!TryValidateModel(skillToPatch))
                {
                    return BadRequest(ModelState);
                }


                // Update attributes in the skill entity
                skill.Name = skillToPatch.Name;
                skill.Description = skillToPatch.Description;
                skill.CategoryID = skillToPatch.CategoryID;
                skill.Level = skillToPatch.Level;
                skill.LastModified = DateTime.UtcNow;
                skill.IsVerified = skillToPatch.IsVerified;


                await _context.SaveChangesAsync();

                return Ok(skill);
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

        [HttpDelete("DeleteSkill/{skillID}", Name = "DeleteSkill")]
        public async Task<ActionResult> DeleteSkill(int skillID)
        {
            try
            {
                var skill = await _context.Skills.FindAsync(skillID);

                if (skill == null)
                {
                    return BadRequest("Skill not found with that ID");
                }

                _context.Skills.Remove(skill);
                await _context.SaveChangesAsync();

                return NoContent();

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

