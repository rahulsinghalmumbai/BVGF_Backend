using BVGFEntities.DTOs;
using BVGFServices.Interfaces.MstMember_Edit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BVGF.Controllers.MstMember_Edit
{
    [Route("api/[controller]")]
    [ApiController]
    public class MstMember_EditController : ControllerBase
    {
        private readonly IMstMember_Edit _mstMember_EditServicse;

        public MstMember_EditController(IMstMember_Edit mstMember_EditServicse )
        {
            _mstMember_EditServicse = mstMember_EditServicse;
        }

        [HttpPost("UpsertMember_Edit")]
        public async Task<IActionResult> Create(MstMemberDto mstMemberDto)
        {
            try
            {
                var respons = await _mstMember_EditServicse.CreateAsync(mstMemberDto);
                return Ok(respons);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }
        //when user clicked edit button
        [HttpGet("GetMember_EditByMemId")]
        public async Task<IActionResult> GetMember_EditByMemId(long MemberId)
        {
            try
            {
                var response= await _mstMember_EditServicse.GetMember_EditByMemId(MemberId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("GetEditedMemberChangesByMemId")]
        public async Task<IActionResult> GetEditedMemberChanges(long MemberId)
        {
            try
            {
                var response = await _mstMember_EditServicse.GetEditedMemberChanges(MemberId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("GetAllEditedMembers")]
        public async Task<IActionResult> GetAllEditedMembers()
        {
            try
            {
                var response = await _mstMember_EditServicse.GetAllEditedMembers();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpPost("ApprovedByAdminOfMemberRecords")]
        public async Task<IActionResult> ApprovedByAdminOfMemberRecords(AdminApprovedDto adminApproved)
        {
            try
            {
                var response = await _mstMember_EditServicse.ApprovedByAdminOfMemberRecords(adminApproved);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminUserLogin(AdminuserDto adminuser)
        {
            try
            {
                var result=await _mstMember_EditServicse.AdminUserLogin(adminuser);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
