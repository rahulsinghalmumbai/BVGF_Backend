using BVGF.Entities;
using BVGFEntities.DTOs;
using BVGFEntities.Entities;
using BVGFRepository.Interfaces.MstCategary;
using BVGFServices.Interfaces.MstCategary;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVGFServices.Services.MstCategary
{
    public class MstCategary : IMstCategary
    {
        private readonly IMstCategary<MstCategory> _repository;
        public MstCategary(IMstCategary<MstCategory> repository)
        {
            _repository = repository;
        }
        public async Task<ResponseEntity> GetAllAsync()
        {
            var result = new List<MstCategoryDto>();
            ResponseEntity respons = new ResponseEntity();
            try
            {
                var dt = await _repository.ExecuteStoredProcedureAsync("stp_GetAllCategories");
                foreach (DataRow row in dt.Rows)
                {
                    result.Add(new MstCategoryDto
                    {
                        CategoryID = row["CategoryID"] != DBNull.Value ? Convert.ToInt32(row["CategoryID"]) : 0,
                        CategoryName = row["CategoryName"]?.ToString(),
                        Status = row["Status"] != DBNull.Value && Convert.ToBoolean(row["Status"]),
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : null,
                        CreatedDt = row["CreatedDt"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDt"]) : null,
                        UpdatedBy = row["UpdatedBy"] != DBNull.Value ? Convert.ToInt32(row["UpdatedBy"]) : null,
                        UpdatedDt = row["UpdatedDt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedDt"]) : null,
                        DeletedBy = row["DeletedBy"] != DBNull.Value ? Convert.ToInt32(row["DeletedBy"]) : null,
                        DeletedDt = row["DeletedDt"] != DBNull.Value ? Convert.ToDateTime(row["DeletedDt"]) : null
                    });
                }
                if (result == null || result.Count == 0)
                {
                    respons.Status = "Success";
                    respons.Message = "Record Not Found";
                    respons.Data = null;
                }
                else
                {
                    respons.Status = "Success";
                    respons.Message = "Found All Category";
                    respons.Data = result;

                }
            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                respons.Status = "Fail";
                respons.Message = message;
                respons.Data = null;
            }
            return respons;
        }


        public async Task<ResponseEntity> CreateAsync(MstCategoryDto cate)
        {
            ResponseEntity respons = new ResponseEntity();
            try
            {
                if (cate == null || string.IsNullOrWhiteSpace(cate.CategoryName))
                { respons.Status = "400"; respons.Message = "Bad Request"; respons.Data = null; };

                var parameters = new SqlParameter[]
                {
                  new SqlParameter("@CategoryID", cate.CategoryID),
                  new SqlParameter("@CategoryName", cate.CategoryName),
                  new SqlParameter("@Status", cate.Status),
                  new SqlParameter("@CreatedBy", cate.CreatedBy),
                  new SqlParameter("@UpdatedBy",cate.UpdatedBy)

                };

                var result = await _repository.ExecuteNonQueryStoredProcedureAsync("stp_InsertCategory", parameters);

                if(cate.CategoryID==0 || cate.CategoryID==null)
                {
                    respons.Status = "Success";
                    respons.Message = "Category Updated Successfully";
                    respons.Data = result;
                }
                else
                {
                    respons.Status = "Success";
                    respons.Message = "Category Created Successfully";
                    respons.Data = result;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                respons.Status = "Fail";
                respons.Message = message;
                respons.Data = null;
            }
            return respons ;
        }


        public async Task<ResponseEntity> GetByID(long ID)
        {
            ResponseEntity respons = new ResponseEntity();
            try
            {
                var parameter = new SqlParameter[]
                {
                 new SqlParameter("@CategoryID",ID)
                };

                var dt = await _repository.ExecuteStoredProcedureAsync("stp_GetCategoryById", parameter);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    var result = new MstCategoryDto
                    {
                        CategoryID = row["CategoryID"] != DBNull.Value ? Convert.ToInt32(row["CategoryID"]) : 0,
                        CategoryName = row["CategoryName"]?.ToString(),
                        Status = row["Status"] != DBNull.Value && Convert.ToBoolean(row["Status"]),
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : null,
                        CreatedDt = row["CreatedDt"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDt"]) : null,
                        UpdatedBy = row["UpdatedBy"] != DBNull.Value ? Convert.ToInt32(row["UpdatedBy"]) : null,
                        UpdatedDt = row["UpdatedDt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedDt"]) : null,
                        DeletedBy = row["DeletedBy"] != DBNull.Value ? Convert.ToInt32(row["DeletedBy"]) : null,
                        DeletedDt = row["DeletedDt"] != DBNull.Value ? Convert.ToDateTime(row["DeletedDt"]) : null
                    };
                   
                    respons.Status = "Success";
                    respons.Message = "Category Found Successfully";
                    respons.Data = result;
                }
                else
                {
                    respons.Status = "Success";
                    respons.Message = "Category Not Found";
                    respons.Data = null;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                respons.Status = "Fail";
                respons.Message = message;
                respons.Data = null;

            }
            return respons;
        }

        public async Task<ResponseEntity> DeleteByID(CategoryDeleteDto deleteDto)
        {
            var response = new ResponseEntity();

            try
            {
                var parameters = new SqlParameter[]
                {
            new SqlParameter("@CategoryID", deleteDto.CategoryID),
            new SqlParameter("@DeletedBy", deleteDto.DeletedBy)
                };


                // Proceed with delete
                var result = await _repository.ExecuteNonQueryStoredProcedureAsync("stp_DeleteCategory", parameters);

                if (result != null && result > 0)
                {
                    response.Status = "Success";
                    response.Message = "Category deleted successfully.";
                    response.Data = result;
                }
                else
                {
                    response.Status = "Fail";
                    response.Message = "Deletion failed. No rows affected.";
                    response.Data = result;
                }
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = $"Exception occurred: {ex.Message}";
                response.Data = null;
            }

            return response;
        }



        public async Task<ResponseEntity> GetDropdownAsync()
        {
            var response = new ResponseEntity();

            try
            {
                var result = new List<CategoryDropDownDto>();

                var dt = await _repository.ExecuteStoredProcedureAsync("stp_categoryDropDown");
                foreach (DataRow row in dt.Rows)
                {
                    result.Add(new CategoryDropDownDto
                    {
                        CategoryID = row["CategoryID"] != DBNull.Value ? Convert.ToInt32(row["CategoryID"]) : 0,
                        CategoryName = row["CategoryName"]?.ToString()

                    });
                }
                if (dt.Rows.Count > 0)
                {
                    response.Status = "Success";
                    response.Message = "DropDown Data";
                    response.Data = result;
                }
                else
                {
                    response.Status = "Success";
                    response.Message = "No DropDown Data";
                    response.Data = result;
                }
            }catch(Exception ex)
            {
                var message = ex.Message.ToString();
                response.Status = "Error";
                response.Message=message;
                response.Data = null;
            }
           
            return response ;

        }


    }
}