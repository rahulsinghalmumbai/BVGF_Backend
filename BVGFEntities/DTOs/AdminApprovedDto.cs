using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVGFEntities.DTOs
{
    public class AdminApprovedDto
    {
        public long? MemberID { get; set; }
        public long? UpdatedBy { get; set; }
        public string? ColumnName { get; set; }
        public string? NewValue { get; set; }
        public string? Flag { get; set; }
    }
}
