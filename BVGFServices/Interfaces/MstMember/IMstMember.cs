using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BVGFEntities.DTOs;
using BVGFEntities.Entities;

namespace BVGFServices.Interfaces.MstMember
{
    public interface IMstMember
    {
        Task<ResponseEntity> GetAllAsync(FilterMemberDto filter);
        Task<ResponseEntity> GetMemberByIdAsync(long MemberId);

        Task<ResponseEntity> CreateAsync(MstMemberDto member);

        Task<ResponseEntity> LoginByMob(string mob);
    }
}
