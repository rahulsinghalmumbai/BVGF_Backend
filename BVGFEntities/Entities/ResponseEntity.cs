using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVGFEntities.Entities
{
    public class ResponseEntity
    {
        public string? Status { get; set; }
        public string? Message { get; set; } 
        public object? Data { get; set; }
    }
}
