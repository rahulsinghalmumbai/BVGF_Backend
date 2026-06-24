using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVGFRepository.Interfaces.MstCategary
{
    public interface IMstCategary<T> where T : class
    {
        Task<DataTable> ExecuteStoredProcedureAsync(string spName, SqlParameter[] parameters = null);
        Task<int> ExecuteNonQueryStoredProcedureAsync(string spName, SqlParameter[] parameters);
       
    }
}
