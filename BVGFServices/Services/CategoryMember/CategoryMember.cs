using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BVGFEntities.DTOs;
using BVGFEntities.Entities;
using BVGFRepository.Interfaces.MstCategary;
using BVGFServices.Interfaces.CategoryMember;
using Microsoft.Data.SqlClient;

namespace BVGFServices.Services.CategoryMember
{
    public class CategoryMember : ICategoryMember
    {
        private readonly IMstCategary<CategoryMember> _repository;
        public CategoryMember(IMstCategary<CategoryMember> repository)
        {
            _repository=repository;
        }
        public async Task<ResponseEntity> CreateAsync(CategoryMemberDto categoryMemberDto)
        {
            ResponseEntity response = new ResponseEntity();
            try
            {
               
                    var parameters = new SqlParameter[]
          {
                new SqlParameter("CategoryMemberID" , categoryMemberDto.CategoryMemberID),
                new SqlParameter("CategoryID" , categoryMemberDto.CategoryID),
                new SqlParameter("MemberID" , categoryMemberDto.MemberID),
                new SqlParameter("CreatedBy" , categoryMemberDto.CreatedBy),
                new SqlParameter("UpdatedBy" , categoryMemberDto.UpdatedBy)
          };

                    var result = await _repository.ExecuteNonQueryStoredProcedureAsync("stp_InsertCategoryMember", parameters);
                if (result != null)
                {
                    if (categoryMemberDto.CategoryMemberID > 0)
                    {
                        response.Status = "Success";
                        response.Message = "Categary  member updated succcessfully";
                        response.Data = result;
                    }
                    else
                    {
                        response.Status = "Success";
                        response.Message = "Categary  member created succcessfully";
                        response.Data = result;
                    }
                }
                 else
                 {
                        response.Status = "Fail";
                        response.Message = "Something went wrong...";
                        response.Data = result;
                 }
              
               
            }catch(Exception ex)
            {
                
                var message = ex.Message.ToString();
                response.Status = "Fail";
                response.Message = message;
                response.Data = null;
            }
            return response;
        }
    }
}
