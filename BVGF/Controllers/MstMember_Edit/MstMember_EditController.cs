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

        [HttpPost]
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
    }
}
