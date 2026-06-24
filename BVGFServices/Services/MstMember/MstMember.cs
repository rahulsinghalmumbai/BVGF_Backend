using Azure;
using BVGFEntities.DTOs;
using BVGFEntities.Entities;
using BVGFRepository.Interfaces.MstCategary;
using BVGFServices.Interfaces.MstMember;
using Microsoft.Data.SqlClient;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace BVGFServices.Services.MstMember
{
    public class MstMember: IMstMember
    {
        private readonly IMstCategary<MstMember> _repositery;

        public MstMember(IMstCategary<MstMember> repositery)
        {
            _repositery = repositery;
        }

       
        public async Task<List<MstMemberDto>> GetAllAsync()
        {
            var result = new List<MstMemberDto>();

            var dt = await _repositery.ExecuteStoredProcedureAsync("stp_GetAllMember");

            foreach (DataRow row in dt.Rows)
            {
                var message = ex.Message.ToString();
                respons.Status = "Fail";
                respons.Message = message;
                respons.Data = null;
            }
            return respons;
        }
    
   

public async Task<ResponseEntity> CreateAsync(MstMemberDto member)
    {
        ResponseEntity response = new ResponseEntity();

        try
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var parameters = new SqlParameter[]
                {
                new SqlParameter("@MemberID", (object?)member.MemberID ?? 0),
                new SqlParameter("@Name", (object?)member.Name ?? DBNull.Value),
                new SqlParameter("@Address", (object?)member.Address ?? DBNull.Value),
                new SqlParameter("@City", (object?)member.City ?? DBNull.Value),
                new SqlParameter("@Mobile1", (object?)member.Mobile1 ?? DBNull.Value),
                new SqlParameter("@Mobile2", (object?)member.Mobile2 ?? DBNull.Value),
                new SqlParameter("@Mobile3", (object?)member.Mobile3 ?? DBNull.Value),
                new SqlParameter("@Telephone", (object?)member.Telephone ?? DBNull.Value),
                new SqlParameter("@Email1", (object?)member.Email1 ?? DBNull.Value),
                new SqlParameter("@Email2", (object?)member.Email2 ?? DBNull.Value),
                new SqlParameter("@Email3", (object?)member.Email3 ?? DBNull.Value),
                new SqlParameter("@Company", (object?)member.Company ?? DBNull.Value),
                new SqlParameter("@CompAddress", (object?)member.CompAddress ?? DBNull.Value),
                new SqlParameter("@CompCity", (object?)member.CompCity ?? DBNull.Value),
                new SqlParameter("@CreatedBy", (object?)member.CreatedBy ?? DBNull.Value),
                new SqlParameter("@UpdatedBy", (object?)member.UpdatedBy ?? DBNull.Value),
                new SqlParameter("@DOB", (object?)member.DOB ?? DBNull.Value),
                };

                int result = await _repositery.ExecuteNonQueryStoredProcedureAsync("stp_InsertMember", parameters);

                if (result !=null)
                {
                    response.Status = "Success";
                    response.Message = member.MemberID > 0 ? "Member updated successfully." : "Member created successfully.";
                    response.Data = member;

                    scope.Complete(); // Commit only if everything is OK
                }
                else
                {
                        response.Status = "Failed";
                        response.Message = member.MemberID > 0 ? "Member Updated Failed." : "Member Created Failed.";
                        response.Data = null;

                }
            }
        }
        catch (SqlException sqlEx)
        {
            if (sqlEx.Message.Contains("UQ_MstMember_Mobile1"))
            {
                response.Status = "Error";
                response.Message = "Mobile1 number already exists.";
                response.Data = null;
            }
            else
            {
                response.Status = "Error";
                response.Message = "Database error occurred.";
                response.Data = sqlEx.Message;
            }
        }
        catch (Exception ex)
        {
            response.Status = "Error";
            response.Message = "An unexpected error occurred.";
            response.Data = ex.Message;
        }

        return response;
    }


    public async Task<ResponseEntity> LoginByMob(string mob)
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
            if(dt.Rows.Count > 0)
            {
                return "Login Successfully";
            }
            else
            {
                return null;
            }
        }

        public async Task<ResponseEntity> GetMemberByIdAsync(long MemberId)
        {
            ResponseEntity response = new ResponseEntity();
            try
            {
                var parameters = new SqlParameter[]
               {
                  new SqlParameter("@MemberID", MemberId)
               };
                var result = await _repositery.ExecuteStoredProcedureAsync("Stp_GetMemberById", parameters);
                if(result.Rows.Count>0)
                {
                    var row = result.Rows[0];
                    var member = new MstMemberDto
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
                        CompCity = row["CompCity"]?.ToString(),
                        DOB = row["DOB"] != DBNull.Value ? Convert.ToDateTime(row["DOB"]) : (DateTime?)null,
                        IsEdit =row["IsEdit"] != DBNull.Value
                        ? Convert.ToBoolean(row["IsEdit"])
                        : (bool?)null,
                        CategoryName = row["CategoryName"]?.ToString()

                    };
                    response.Status = "Success";
                    response.Message = "Member fetched successfully";
                    response.Data = member;
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Member not found or something went wrong.";
                    response.Data = null;
                }
            }
            catch (Exception ex)
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
