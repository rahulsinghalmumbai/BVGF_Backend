
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

                if (categories == null || categories.Count == 0)
                    return NotFound(new ResponseEntity
                    {
                        Status = "404",
                        Message = "No categories found",
                        Data = null
                    });

                return Ok(new ResponseEntity
                {
                    Status = "200",
                    Message = "Categaries Founds",
                    Data = categories
                });
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
                if (dto == null || string.IsNullOrWhiteSpace(dto.CategoryName))
                    return BadRequest(new ResponseEntity { Status = "400", Message = "Bad Request", Data = null });

                var result = await _mstCategaryService.CreateAsync(dto);

                if (dto.CategoryID == 0 || dto.CategoryID == null)
                {
                    return Ok(new ResponseEntity { Status = "200", Message = "Category created successfully", Data = result });
                }
                else
                {
                    return Ok(new ResponseEntity { Status = "200", Message = "Category Updated successfully", Data = result });
                }
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

                if (category == null)
                {
                    return Ok(new ResponseEntity
                    {
                        Status = "200",
                        Message = "No category found",
                        Data = category
                    });
                }

                return Ok(new ResponseEntity
                {
                    Status = "200",
                    Message = "Category found",
                    Data = category
                });
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


        [HttpDelete("CategoryID")]
        public async Task<ActionResult> DeleteCategoryByID(long CategoryID)
        {
            try
            {
                var category = await _mstCategaryService.DeleteByID(CategoryID);

               if(category>0)
                {
                    return Ok(new ResponseEntity
                    {
                        Status = "200",
                        Message = "category Delete",
                        Data = category
                    });
                }
                else
                {
                    return Ok(new ResponseEntity
                    {
                        Status = "200",
                        Message = "Category not found or already deleted",
                        Data = category
                    });
                }
               
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }


        }


    }
}