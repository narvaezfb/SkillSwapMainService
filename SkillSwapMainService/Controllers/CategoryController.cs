using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkillSwapMainService.Models;
using SkillSwapMainService.Models.RequestModels.Category;

namespace SkillSwapMainService.Controllers
{
    [Route("Admin/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly SkillSwapDbContext _context;

        public CategoryController(SkillSwapDbContext context)
        {
            _context = context;
        }

        [HttpGet("Retrieve" , Name="Retrieve Categories")]
        public ActionResult RetrieveCategories()
        {
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("Retrieve/{categoryId}" ,Name= "Retrieve Category by ID")]
        public async Task<ActionResult> RetrieveOneCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if( category == null)
            {
                return BadRequest("Not Category found with that ID");
            }

            return Ok(category);
        }

        [HttpPost("Create", Name =" Create Category")]
        public async Task<ActionResult> CreateCategory([FromBody]CreateCategoryRequest createCategoryRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid Category Body");
            }

            Category category = new Category
            {
                Name = createCategoryRequest.Name,
                Description = createCategoryRequest.Description
            };

            await _context.AddAsync(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }
        
        [HttpDelete("Delete/{categoryId}", Name ="Delete one category")]
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (category == null)
            {
                return BadRequest("Not Category found with that ID");
            }

            _context.Remove(categoryId);
            await _context.SaveChangesAsync();

            return Ok("Category Deleted");
        }
    }
}

