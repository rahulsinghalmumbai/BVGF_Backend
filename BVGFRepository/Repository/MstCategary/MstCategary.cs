using BVGF.Entities;
using BVGFRepository.Interfaces.MstCategary;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NPOI.SS.Formula.Functions;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVGFRepository.Repository.MstCategary
{
    public class MstCategary<T> : IMstCategary<T> where T : class
    {

        private readonly string _connectionString;
        public MstCategary(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                 ?? "your-default-fallback-connection-string";

        }
        public async Task<DataTable> ExecuteStoredProcedureAsync(string spName, SqlParameter[] parameters = null)
        {
            var dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw; 
            }
            catch (Exception ex)
            {
                throw;
            }

            return dt;
        }

        public async Task<int> ExecuteNonQueryStoredProcedureAsync(string spName, SqlParameter[] parameters)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    await conn.OpenAsync();
                    rowsAffected = await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                
                Console.WriteLine($"SQL Exception: {ex.Message}");
                throw; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception: {ex.Message}");
                throw; 
            }

            return rowsAffected;
        }


        //public async Task<MstMember> ExecuteStoredProcedureWithResultAsync(string spName, SqlParameter[] parameters)
        //{
        //    using var conn = new SqlConnection(_connectionString);
        //    using var cmd = new SqlCommand(spName, conn)
        //    {
        //        CommandType = CommandType.StoredProcedure
        //    };
        //    cmd.Parameters.AddRange(parameters);

        //    await conn.OpenAsync();
        //    using var reader = await cmd.ExecuteReaderAsync();

        //    if (await reader.ReadAsync())
        //    {
        //        return new MstMember
        //        {
        //            MemberID = reader.GetInt64(reader.GetOrdinal("MemberID")),
        //            Name = reader["Name"]?.ToString(),
        //            Address = reader["Address"] as string,
        //            City = reader["City"] as string,
        //            Mobile1 = reader["Mobile1"]?.ToString(),
        //            Mobile2 = reader["Mobile2"] as string,
        //            Mobile3 = reader["Mobile3"] as string,
        //            Telephone = reader["Telephone"] as string,
        //            Email1 = reader["Email1"]?.ToString(),
        //            Email2 = reader["Email2"] as string,
        //            Email3 = reader["Email3"] as string,
        //            Company = reader["Company"] as string,
        //            CompAddress = reader["CompAddress"] as string,
        //            CompCity = reader["CompCity"] as string,
        //            DOB = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]) : (DateTime?)null,
        //            Status = reader["Status"] != DBNull.Value && Convert.ToBoolean(reader["Status"]),
        //            CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt64(reader["CreatedBy"]) : (long?)null,
        //            CreatedDt = reader["CreatedDt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDt"]) : (DateTime?)null,
        //            UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToInt64(reader["UpdatedBy"]) : (long?)null,
        //            UpdatedDt = reader["UpdatedDt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedDt"]) : (DateTime?)null,
        //            DeletedBy = reader["DeletedBy"] != DBNull.Value ? Convert.ToInt64(reader["DeletedBy"]) : (long?)null,
        //            DeletedDt = reader["DeletedDt"] != DBNull.Value ? Convert.ToDateTime(reader["DeletedDt"]) : (DateTime?)null,
        //            //IsEdit = reader["IsEdit"] != DBNull.Value && Convert.ToBoolean(reader["IsEdit"])
        //        };
        //    }

        //    return null;
        //}


    }
}
