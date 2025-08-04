using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BVGFEntities.DTOs;
using BVGFEntities.Entities;

namespace BVGFServices.Interfaces.MstMember_Edit
{
    public interface IMstMember_Edit
    {
        Task<ResponseEntity> CreateAsync(MstMemberDto member);
    }
}
