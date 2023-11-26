using Microsoft.AspNetCore.Mvc;
using SkillSwapMainService.Models;
using SkillSwapMainService.Models.RequestModels;
using SkillSwapMainService.Models.Mappers;
using Microsoft.AspNetCore.JsonPatch;

namespace SkillSwapMainService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
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

                //Map request to entity
                Skill skill = _skillMapper.MapToSkillEntity(createSkillRequest);

                // Add new skill and save it to DB async
                _context.Skill.Add(skill);
                await _context.SaveChangesAsync();

                return Ok(skill);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet("GetSkill/{SkillID}", Name ="GetSkill")]
        public async Task<ActionResult> GetSkill(int SkillID)
        {
            var skill = await _context.Skill.FindAsync(SkillID);

            if (skill == null)
            {
                return BadRequest("Skill not found with that ID");
            }

            return Ok(skill);
        }

        [HttpPatch("UpdateSkill/{SkillID}", Name = "UpdateSkill")]
        public async Task<ActionResult> UpdateSkill(int SkillID, [FromBody] JsonPatchDocument<SkillUpdateRequest> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Not model included in payload");
            }

            var skill = await _context.Skill.FindAsync(SkillID);

            if (skill == null )
            {
                return BadRequest("Not skill found with that ID");
            }

            var skillToPatch = new SkillUpdateRequest
            {
                Name = skill.Name,
                Description = skill.Description,
                Category = skill.Category,
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
            skill.Category = skillToPatch.Category;
            skill.Level = skillToPatch.Level;
            skill.LastModified = DateTime.UtcNow;
            skill.IsVerified = skillToPatch.IsVerified;
            

            await _context.SaveChangesAsync();

            return Ok(skill);
        }

        [HttpDelete("DeleteSkill/{SkillID}", Name = "DeleteSkill")]
        public async Task<ActionResult> DeleteSkill(int skillID)
        {
            var skill = await _context.Skill.FindAsync(skillID);

            if(skill == null)
            {
                return BadRequest("Skill not found with that ID");
            }

            _context.Skill.Remove(skill);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

