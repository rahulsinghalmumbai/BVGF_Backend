using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVGFEntities.DTOs
{
    public class CategoryDeleteDto
    {
        public long CategoryID { get; set; }
        public long DeletedBy { get; set; }
    }
}
