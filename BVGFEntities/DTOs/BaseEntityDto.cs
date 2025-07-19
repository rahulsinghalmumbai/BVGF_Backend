using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVGFEntities.DTOs
{
    public class BaseEntityDto
    {
        public bool Status { get; set; } = true;

        public long? CreatedBy { get; set; }
        public DateTime? CreatedDt { get; set; }

        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDt { get; set; }

        public long? DeletedBy { get; set; }
        public DateTime? DeletedDt { get; set; }
    }
}
