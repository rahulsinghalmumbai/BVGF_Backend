using BVGF.Entities;
using BVGFEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVGFServices.Interfaces.MstCategary
{
    public  interface IMstCategary
    {
        Task<List<MstCategoryDto>> GetAllAsync();

        Task<string> CreateAsync(MstCategoryDto category);

        Task<MstCategoryDto> GetByID(long ID);

        Task<long> DeleteByID(long ID);

    }
}
