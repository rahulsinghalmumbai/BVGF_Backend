using BVGF.Entities;
using BVGFEntities.DTOs;
using BVGFEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVGFServices.Interfaces.MstCategary
{
    public  interface IMstCategary
    {
        Task<ResponseEntity> GetAllAsync();

        Task<ResponseEntity> CreateAsync(MstCategoryDto category);

        Task<ResponseEntity> GetByID(long ID);

        Task<ResponseEntity> DeleteByID(CategoryDeleteDto dto);

        Task<ResponseEntity> GetDropdownAsync();
    }
}
