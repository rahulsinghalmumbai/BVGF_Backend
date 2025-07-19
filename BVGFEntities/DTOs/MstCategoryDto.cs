using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVGFEntities.DTOs
{
    public class MstCategoryDto:BaseEntityDto
    {
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
