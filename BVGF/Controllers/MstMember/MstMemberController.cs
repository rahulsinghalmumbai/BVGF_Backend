using BVGFEntities.DTOs;
using BVGFEntities.Entities;
using BVGFServices.Interfaces.MstMember;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BVGF.Controllers.MstMember
{
    [Route("api/[controller]")]
    [ApiController]
    public class MstMemberController : ControllerBase
    {
        private readonly IMstMember _mstMemberService;

        public MstMemberController(IMstMember mstMemberService)
        {
            _mstMemberService = mstMemberService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllMember([FromQuery] FilterMemberDto filter)
        {
            try
            {
                var Members = await _mstMemberService.GetAllAsync(filter);
                if (Members == null || Members.TotalCount == 0)
                {
                    return NotFound(new ResponseEntity
                    {
                        Status = "400",
                        Message = "Record Not Found",
                        Data = Members
                    });
                }
                return Ok(new ResponseEntity
                {
                    Status = "200",
                    Message = "All Member Found",
                    Data = Members
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }

        }

        [HttpPost("UpsertMember")]
        public async Task<IActionResult> CreateMember(MstMemberDto member)
        {
            try
            {
                if (member == null || String.IsNullOrWhiteSpace(member.Name))
                {
                    return BadRequest(new ResponseEntity { Status = "400", Message = "Bad Request", Data = null });
                }

                var data = await _mstMemberService.CreateAsync(member);

                if (member.MemberID == 0 || member.MemberID==null) 
                {
                    return Ok(new ResponseEntity
                    {
                        Status = "200",
                        Message = "Create Member Successfully",
                        Data = data
                    });
                }
                else
                {
                    return Ok(new ResponseEntity
                    {
                        Status = "200",
                        Message = "Update Member Successfully",
                        Data = data
                    });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError , $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("login")]
        public async Task<ActionResult> LoginMember([FromQuery] string MobileNo)
        {
            try
            {
                string result = await _mstMemberService.LoginByMob(MobileNo);
                if (result == null)
                {
                    return NotFound(new ResponseEntity
                    {
                        Message = "Something went wrong..",
                        Status = "400",
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ResponseEntity
                    {
                        Message = "Login Successfully",
                        Status = "200",
                        Data = result
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
