using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BVGFEntities.DTOs;

namespace BVGFServices.Interfaces.MstMember
{
    public interface IMstMember
    {
        Task<ResponseEntity> GetAllAsync(FilterMemberDto filter);
        Task<ResponseEntity> GetMemberByIdAsync(long MemberId);

        Task<string> CreateAsync(MstMemberDto member);

        Task<string> LoginByMob(string mob);
    }
}
