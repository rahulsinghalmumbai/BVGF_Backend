using BVGF.Entities;
using BVGFEntities.DTOs;
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
        public async Task<List<MstCategoryDto>> GetAllAsync()
        {
            var result = new List<MstCategoryDto>();

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
            return result;
        }


        public async Task<string> CreateAsync(MstCategoryDto cate)
        {
            var parameters = new SqlParameter[]
         {
            new SqlParameter("@CategoryID", cate.CategoryID),
            new SqlParameter("@CategoryName", cate.CategoryName),
            new SqlParameter("@Status", cate.Status),
            new SqlParameter("@CreatedBy", cate.CreatedBy),
            new SqlParameter("@UpdatedBy",cate.UpdatedBy)
             
         };

            var result = await _repository.ExecuteNonQueryStoredProcedureAsync("stp_InsertCategory", parameters);
            return result.ToString() ;
        }


        public async Task<MstCategoryDto> GetByID(long ID)
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
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<long> DeleteByID(long ID)
        {
            var parameters = new SqlParameter[]
            {
            new SqlParameter("@CategoryID", ID)

            };
            if(ID>0 && ID!=null)
            {

                var getData = await _repository.ExecuteStoredProcedureAsync("stp_GetCategoryById", parameters);
                if(getData.Rows.Count>0)
                {
                    var parameters1 = new SqlParameter[]
                    {
                        new SqlParameter("@CategoryID", ID)

                    };
                    var result = await _repository.ExecuteNonQueryStoredProcedureAsync("stp_DeleteCategory", parameters1);
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
         
        }


    }
}