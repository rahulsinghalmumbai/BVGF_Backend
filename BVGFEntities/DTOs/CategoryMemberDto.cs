using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BVGF.Entities;

namespace BVGFEntities.DTOs
{
    public class CategoryMemberDto : BaseEntity
    {
        public long CategoryMemberID { get; set; }

        public long CategoryID { get; set; }
        public long MemberID { get; set; }
    }
}
