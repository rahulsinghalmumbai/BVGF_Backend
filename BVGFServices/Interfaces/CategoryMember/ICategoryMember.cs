using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BVGFEntities.DTOs;
using BVGFEntities.Entities;

namespace BVGFServices.Interfaces.CategoryMember
{
    public interface ICategoryMember
    {
        Task<ResponseEntity> CreateAsync(CategoryMemberDto categoryMemberDto);
    }
}
