using BVGFEntities.DTOs;
using BVGFEntities.Entities;
using BVGFRepository.Interfaces.MstCategary;
using BVGFRepository.Repository.MstCategary;
using BVGFServices.Interfaces.MstMember_Edit;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                //new SqlParameter("@Status", member.Status),
                new SqlParameter("@CreatedBy", member.CreatedBy),
                new SqlParameter("@UpdatedBy", member.UpdatedBy),
                new SqlParameter("@DOB", member.DOB)

              };
                var result = await _repository.ExecuteNonQueryStoredProcedureAsync("stp_Upsert_MstMemberEdit", parameters);
                if(result != null && result>0 || result == 1)
                {

                    response.Status = "Success";
                    response.Message = "Member Updated Successfully";
                    response.Data = member;
                    
                }
                else if(result == -1)
                {
                    response.Status = "Failed";
                    response.Message = "Member is Updated Something went wrong..";
                    response.Data = null;
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Member is Updated Something went wrong..";
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                var message=ex.Message.ToString();
                response.Message = message;
                response.Data = null;
                response.Status = "Failed";
            }
            return response;
        }

        public async Task<ResponseEntity> GetMember_EditByMemId(long MemberId)
        {
            ResponseEntity response = new ResponseEntity();
            try
            {
                var parameters = new SqlParameter[]
                {
                 new SqlParameter("@MemberID", MemberId)
                };

                var result = await _repository.ExecuteStoredProcedureAsync("stp_GetMemberEditDataByMemberID", parameters);

                if (result.Rows.Count > 0)
                {
                    var row = result.Rows[0]; 

                    var member = new MstMember_EditDto
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
                        IsEdit = ColumnExists(result, "IsEdit") && row["IsEdit"] != DBNull.Value
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
                response.Status = "Failed";
                response.Message = ex.Message;
                response.Data = null;
            }

            return response;
        }
        private bool ColumnExists(DataTable table, string columnName)
        {
            return table.Columns.Contains(columnName);
        }
        public async Task<ResponseEntity> GetEditedMemberChanges(long MemberId)
        {
            ResponseEntity response = new ResponseEntity();
            try
            {
                var parameters = new SqlParameter[]
                { new SqlParameter("@MemberID",MemberId)};
                var result = await _repository.ExecuteStoredProcedureAsync("stp_GetEditedMemberChanges", parameters);
                var memberEditList = new List<EditedMemberChangeDto>();
                if (result.Rows.Count>0)
                {
                    foreach (DataRow row in result.Rows)
                    {
                        memberEditList.Add(new EditedMemberChangeDto
                        {
                            MemberId =Convert.ToInt64(row["MemberId"]),
                            ColumnName = row["ColumnName"].ToString(),
                            NewValue = row["NewValue"].ToString(),
                            OldValue = row["OldValue"].ToString()
                        });
                    }
                    response.Status = "Success";
                    response.Message = "Updated Member Fetch Successfully..";
                    response.Data = memberEditList;
                }
                else
                {
                    response.Status = "Success";
                    response.Message = "Not Updated any types value";
                    response.Data = null;
                }

            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                var message = ex.Message.ToString();
                response.Message = message;
                response.Data = null;
                
            }
            return response;
        }

        public async Task<ResponseEntity> GetAllEditedMembers()
        {
            ResponseEntity response = new ResponseEntity();
            try
            {
                var result = await _repository.ExecuteStoredProcedureAsync("stp_GetAllEditedMembers");

                var members = new List<MstMember_EditDto>();

                foreach (DataRow row in result.Rows)
                {
                    members.Add(new MstMember_EditDto
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
                        DOB = row["DOB"] != DBNull.Value ? Convert.ToDateTime(row["DOB"]) : (DateTime?)null
                      
                    });
                }

                response.Status = "Success";
                response.Message = "Edited members fetched successfully.";
                response.Data = members;
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message;
                response.Data = null;
            }

            return response;
        }

        public async Task<ResponseEntity> ApprovedByAdminOfMemberRecords(AdminApprovedDto adminApproved)
        {
            ResponseEntity response = new ResponseEntity();
            try
            {
                var parameters = new SqlParameter[]
                { new SqlParameter("@MemberID",adminApproved.MemberID),
                  new SqlParameter("@UpdatedBy",adminApproved.UpdatedBy),
                  new SqlParameter("@ColumnName",adminApproved.ColumnName),
                  new SqlParameter("@NewValue",adminApproved.NewValue),
                  new SqlParameter("@Flag",adminApproved.Flag)
                };
                var result = await _repository.ExecuteNonQueryStoredProcedureAsync("stp_UpdateMemberFromEditAndDeleteEditRow", parameters);
                if(result!=null && adminApproved.Flag== "Approved")
                {
                    response.Status = "Success";
                    response.Message = "Member Approved Successfully..";
                    response.Data = result;
                }
                else if(result != null && adminApproved.Flag == "Rejected")
                {
                    response.Status = "Success";
                    response.Message = "Member Rejected Successfully..";
                    response.Data = result;
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Something Went Wrong..";
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message;
                response.Data = null;
            }

            return response;
        }

        public async Task<ResponseEntity> AdminUserLogin(AdminuserDto member)
        {
            ResponseEntity response = new ResponseEntity();
            try
            {
                var parameters = new SqlParameter[]
                { new SqlParameter("@UserName",member.UserName),new SqlParameter("@pwd",member.pwd)};
                var result = await _repository.ExecuteStoredProcedureAsync("Stp_AdminLogin", parameters);
                
                if(result!=null && result.Rows.Count>0)
                {
                    DataRow row = result.Rows[0];
                    Adminuser adminuser = new Adminuser
                    {
                        Id = Convert.ToInt64(row["Id"]),
                        UserName = row["UserName"].ToString()
                        //pwd = row["pwd"].ToString()
                    };
                    response.Status = "Success";
                    response.Message = "Login Successfully..";
                    response.Data = adminuser;
                }
                else if(result.Rows.Count==0)
                {
                    response.Status = "Failed";
                    response.Message = "Credentials do not match.";
                    response.Data = null;
                }else
                {
                    response.Status = "Failed";
                    response.Message = "Something Went wrong.";
                    response.Data = null;
                }
    
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message;
                response.Data = null;
            }

            return response;
        }
    }
}
