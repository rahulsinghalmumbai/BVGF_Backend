using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BVGFEntities.DTOs;
using BVGFRepository.Interfaces.MstCategary;
using BVGFServices.Interfaces.MstMember;
using Microsoft.Data.SqlClient;

namespace BVGFServices.Services.MstMember
{
    public class MstMember: IMstMember
    {
        private readonly IMstCategary<MstMember> _repositery;
       
        public MstMember(IMstCategary<MstMember> repositery)
        {
            _repositery = repositery;
           
        }

        public async Task<MemberListResultDto> GetAllAsync(FilterMemberDto filter)
        {
            var result = new MemberListResultDto
            {
                Members = new List<MstMemberDto>(),
                TotalCount = 0
            };

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", string.IsNullOrEmpty(filter.Name) ? DBNull.Value : (object)filter.Name),
                new SqlParameter("@City", string.IsNullOrEmpty(filter.City) ? DBNull.Value : (object)filter.City),
                new SqlParameter("@Company", string.IsNullOrEmpty(filter.Company) ? DBNull.Value : (object)filter.Company),
                new SqlParameter("@Mobile", string.IsNullOrEmpty(filter.Mobile1) ? DBNull.Value : (object)filter.Mobile1)
            };

            var ds = await _repositery.ExecuteStoredProcedureAsync("stp_GetAllMember", parameters);

            if (ds.Rows.Count > 0)
            {
                foreach (DataRow row in ds.Rows)
                {
                    result.Members.Add(new MstMemberDto
                    {
                        MemberID = row["MemberID"] != DBNull.Value ? Convert.ToInt32(row["MemberID"]) : 0,
                        Name = row["Name"]?.ToString(),
                        Address = row["Address"]?.ToString(),
                        City = row["City"]?.ToString(),
                        Mobile1 = row["Mobile1"]?.ToString(),
                        Mobile2 = row["Mobile2"]?.ToString(),
                        Mobile3 = row["Mobile3"]?.ToString(),
                        Telephone = row["Telephone"]?.ToString(),
                        Email1 = row["Email1"]?.ToString(),
                        Email2 = row["Email2"]?.ToString(),
                        Email3 = row["Email3"]?.ToString(),
                        Company = row["Company"]?.ToString(),
                        CompAddress = row["CompAddress"]?.ToString(),
                        CompCity = row["CompCity"]?.ToString()
                    });
                }
                result.TotalCount = Convert.ToInt32(ds.Rows[0]["TotalCount"]);
            }

            return result;
        }


        public async Task<string> CreateAsync(MstMemberDto member)
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

            var result = await _repositery.ExecuteNonQueryStoredProcedureAsync("stp_InsertMember",parameters);
            return result.ToString();
        }



        public async Task<string> LoginByMob(string mob)
        {
            var parameter = new SqlParameter[]
            {
                new SqlParameter("@Mobile1",mob)
            };
            var dt = await _repositery.ExecuteStoredProcedureAsync("stp_loginMember", parameter);
            if (dt.Rows.Count > 0)
            {
                return "Login Successfully";
            }
            else
            {
                return null;
            }
        }

    }
}
