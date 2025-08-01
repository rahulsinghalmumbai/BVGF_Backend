using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BVGFEntities.DTOs;
using BVGFEntities.Entities;
using BVGFRepository.Interfaces.MstCategary;
using BVGFRepository.Repository.MstCategary;
using BVGFServices.Interfaces.MstMember_Edit;
using Microsoft.Data.SqlClient;

namespace BVGFServices.Services.MstMember_Edit
{
    public class MstMember_Edit : IMstMember_Edit
    {
        private readonly IMstCategary<MstMember_Edit> _repository;

        public MstMember_Edit(IMstCategary<MstMember_Edit> repository)
        {
            _repository = repository;
        }
        public async Task<ResponseEntity> CreateAsync(MstMemberDto member)
        {
            try
            {
                var parameters = new SqlParameter[]
              {
                new SqlParameter("@MemberID", member.MemberID),
                new SqlParameter("@Name", member.Name),
                new SqlParameter("@Address", member.Address),
                new SqlParameter("@City", member.City),
                new SqlParameter("@Mobile1", member.Mobile1),
                new SqlParameter("@Mobile2", member.Mobile2),
                new SqlParameter("@Mobile3", member.Mobile3),
                new SqlParameter("@Telephone", member.Telephone),
                new SqlParameter("@Email1", member.Email1),
                new SqlParameter("@Email2", member.Email2),
                new SqlParameter("@Email3", member.Email3),
                new SqlParameter("@Company", member.Company),
                new SqlParameter("@CompAddress", member.CompAddress),
                new SqlParameter("@CompCity", member.CompCity),
                new SqlParameter("@Status", member.Status),
                new SqlParameter("@CreatedBy", member.CreatedBy),
                new SqlParameter("@UpdatedBy", member.UpdatedBy),
                new SqlParameter("@DOB", member.DOB)

              };
                var result = await _repository.ExecuteNonQueryStoredProcedureAsync("stp_InsertMstMember_Edit", parameters);
                if(result != null)
                {
                    
                   return new ResponseEntity
                   {
                      Status = "200",
                      Message = "Creted Successfully",
                      Data = member
                   };
                    
                    
                }
                else
                {
                    return new ResponseEntity
                    {
                        Status = "400",
                        Message = "Bad Request",
                        Data = member
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
