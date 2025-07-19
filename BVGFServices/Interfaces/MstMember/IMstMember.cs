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
        Task<List<MstMemberDto>> GetAllAsync();

        Task<string> CreateAsync(MstMemberDto member);
    }
}
