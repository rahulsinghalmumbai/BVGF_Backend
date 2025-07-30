using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVGFEntities.DTOs
{
    public class MemberListResultDto
    {
        public List<MstMemberDto> Members { get; set; } = new();
        public int? TotalCount { get; set; }
        
    }
}
