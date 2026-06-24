using BVGFEntities.DTOs;
using BVGFEntities.Entities;
using BVGFServices.Interfaces.CategoryMember;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BVGF.Controllers.CategoryMember
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryMemberController : ControllerBase
    {
        private readonly ICategoryMember _categoryMember;

        public CategoryMemberController(ICategoryMember categoryMember)
        {
            _categoryMember = categoryMember;
        }

        [HttpPost("UpsertCatMember")]
        public async Task<IActionResult> CreateCategoryMember([FromBody] CategoryMemberDto dto)
        {
            try
            {
                var result = await _categoryMember.CreateAsync(dto);
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
                
               
        }
    }
}
