using BVGFEntities.DTOs;
using BVGFEntities.Entities;
using BVGFRepository.Interfaces.MstCategary;
using BVGFServices.Interfaces.MstMember;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVGFServices.Services.MstMember
{
    public class MstMember: IMstMember
    {
        private readonly IMstCategary<MstMember> _repositery;
       
        public MstMember(IMstCategary<MstMember> repositery)
        {
            _repositery = repositery;
           
        }

        public async Task<ResponseEntity> GetAllAsync(FilterMemberDto filter)
        {
            ResponseEntity respons = new ResponseEntity();
            try
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
                    new SqlParameter("@Mobile", string.IsNullOrEmpty(filter.Mobile1) ? DBNull.Value : (object)filter.Mobile1),
                    new SqlParameter("@CatId", !filter.CatName.HasValue || filter.CatName == 0 ? DBNull.Value : (object)filter.CatName)
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
                            CompCity = row["CompCity"]?.ToString(),
                            CategoryName = row["CategoryName"]?.ToString()
                        });
                    }
                    result.TotalCount = Convert.ToInt32(ds.Rows[0]["TotalCount"]);
                }
                if(result == null || result.TotalCount ==0)
                {
                    respons.Status = "Success";
                    respons.Message = "Record Not Found";
                    respons.Data = null;
                }
                else
                {
                    respons.Status = "Success";
                    respons.Message = "Found All Members";
                    respons.Data = result;
                   
                }
               
            }
            catch(Exception ex)
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
                    new SqlParameter("@DOB", member.DOB),
                };

                int result = await _repositery.ExecuteNonQueryStoredProcedureAsync("stp_InsertMember", parameters);

                if (result > 0)
                {
                    response.Status = "Success";
                    response.Message = member.MemberID > 0 ? "Member updated successfully." : "Member created successfully.";
                    response.Data = member;
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Something went wrong while saving member data.";
                    response.Data = null;
                }
            }
            catch (SqlException sqlEx)
            {
               
                response.Status = "Error";
                response.Message = "Database error occurred.";
                response.Data = sqlEx.Message; 
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
            ResponseEntity respons = new ResponseEntity();
            try
            {
                MstMemberDto result = null;

                var parameter = new SqlParameter[]
                {
                   new SqlParameter("@Mobile1", mob)
                };

                var dt = await _repositery.ExecuteStoredProcedureAsync("stp_loginMember", parameter);

                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    result = new MstMemberDto
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
                        IsEdit = row["IsEdit"] != DBNull.Value ? Convert.ToBoolean(row["IsEdit"]) : (bool?)null,
                    };

                    respons.Status = "Success";
                    respons.Message = "Login Successfully";
                    respons.Data = result;
                }
                else
                {
                    respons.Status = "400";
                    respons.Message = "Something went wrong.";
                    respons.Data = null;
                }


            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                respons.Status = "Fail";
                respons.Message = message;
                respons.Data=null;
            }

            return respons;
        }


    }
}
