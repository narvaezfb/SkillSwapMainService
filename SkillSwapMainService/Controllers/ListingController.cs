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
    [Route("User/[Controller]")]
    public class ListingController : ControllerBase
	{
        private readonly SkillSwapDbContext _context;

        public ListingController(SkillSwapDbContext context)
		{
            _context = context;
        }

        [HttpPost("CreateListing", Name = "CreateListing")]
        public async Task<ActionResult> CreateListing([FromBody] CreateLisitingRequest createLisitingRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Request Data");
                }

                Listing listing = new Listing
                {
                    UserID = createLisitingRequest.UserId,
                    SkillID = createLisitingRequest.SkillId,
                    Description = createLisitingRequest.Description,
                    Location = createLisitingRequest.Location,
                    Availability = createLisitingRequest.Availability
                };

                _context.Listings.Add(listing);
                await _context.SaveChangesAsync();

                return Ok(listing);
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
        [HttpGet("GetListing/{listingId}", Name = "GetListing")]
        public async Task<ActionResult> GetListing(int listingId)
        {
            try
            {
                var listing = await _context.Listings.FindAsync(listingId);

                if (listing == null)
                {
                    return BadRequest("No listing found with that ID");
                }

                var skill = await _context.Skills.FindAsync(listing.SkillID);

                if (skill != null)
                {
                    listing.Skill = skill;
                }
                return Ok(listing);
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
        [HttpDelete("DeleteListing/{listingId}", Name ="Delete Listing")]
        public async Task<ActionResult> DeleteListing(int listingId)
        {
            try
            {
                var listing = await _context.Listings.FindAsync(listingId);

                if(listing == null)
                {
                    return BadRequest("No listing found with that ID");
                }

                _context.Listings.Remove(listing);
                await _context.SaveChangesAsync();

                return Ok("Listing has been deleted");
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

