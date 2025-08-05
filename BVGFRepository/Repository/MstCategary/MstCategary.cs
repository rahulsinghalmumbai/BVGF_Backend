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
    }
}
