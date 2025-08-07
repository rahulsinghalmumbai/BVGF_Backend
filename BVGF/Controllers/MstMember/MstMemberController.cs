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
        public async Task<IActionResult> GetAllMember([FromQuery] FilterMemberDto filter)
        {
            try
            {
                var Members = await _mstMemberService.GetAllAsync(filter);
               return Ok(Members);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }

        }
        [HttpGet("MemberById")]
        public async Task<IActionResult> GetMemberByIdAsync(long MemberId)
        {
            try
            {
                var result = await _mstMemberService.GetMemberByIdAsync(MemberId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }
            
        [HttpPost("UpsertMember")]
        public async Task<IActionResult> CreateMember([FromBody] MstMemberDto member)
        {
            try
            {
                if (member == null || String.IsNullOrWhiteSpace(member.Name))
                {
                    return BadRequest(member);
                }

                var data = await _mstMemberService.CreateAsync(member);
                if (data.Status == "Failed" || data.Status == "Error")
                {
                    return BadRequest(data);
                }

                return Ok(data);

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
                var result = await _mstMemberService.LoginByMob(MobileNo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
