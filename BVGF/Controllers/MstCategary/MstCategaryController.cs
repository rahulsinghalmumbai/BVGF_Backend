
using BVGF.Entities;
using BVGFEntities.DTOs;
using BVGFEntities.Entities;
using BVGFServices.Interfaces.MstCategary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BVGF.Controllers.MstCategary
{
    [Route("api/[controller]")]
    [ApiController]
    public class MstCategaryController : ControllerBase
    {
        private readonly IMstCategary _mstCategaryService;
        public MstCategaryController(IMstCategary mstCategaryService)
        {
            _mstCategaryService = mstCategaryService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _mstCategaryService.GetAllAsync();
                
                return Ok(categories);
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("upsert")]
        public async Task<IActionResult> CreateCategory([FromBody] MstCategoryDto dto)
        {
            try
            {
                var result = await _mstCategaryService.CreateAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpGet("{CategoryID}")]
        public async Task<ActionResult> GetCategoryByID(long CategoryID)
        {
            try
            {
                var category = await _mstCategaryService.GetByID(CategoryID);
                return Ok(category);
               
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseEntity
                {
                    Status = "500",
                    Message = "Internal Server Error",
                    Data = ex.Message
                });
            }
        }


        [HttpPost("CategoryID")]
        public async Task<IActionResult> DeleteCategoryByID([FromBody] CategoryDeleteDto dto)
        {
            try
            {
                var category = await _mstCategaryService.DeleteByID(dto);

                return Ok(category);
               
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }


        }


        [HttpGet("DropDown")]
        public async Task<ActionResult> GetDropDown()
        {
            try
            {
                var data = await _mstCategaryService.GetDropdownAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error:{ex.Message}");
            }
        }


    }
}