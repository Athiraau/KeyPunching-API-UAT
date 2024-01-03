using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using Dapper.Oracle;
using DataAccess.Context;
using DataAccess.Dto;
using Oracle.ManagedDataAccess.Types;
using DataAccess.Contracts;
using DataAccess.Dto.Request;
using DataAccess.Entities;

namespace DataAccess.Repository
{
    public class KeyPunchGetDetails :IKeyPunchGetDetails
    {
        private DapperContext _dapperContext;
        private DtoWrapper _dto;
        //private ErrorDetails _error;

        public KeyPunchGetDetails(DapperContext context, DtoWrapper dto)
        {
            _dapperContext= context;
            _dto = dto;
           // _error = error;
        }

        public async Task<dynamic> GetKeyEmpData(string flag, string indata)
        {
            OracleRefCursor result = null;
            var procedureName = "proc_branch_key_get_data";
            var parameters = new OracleDynamicParameters(); 
            parameters.Add("p_flag", flag, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_indata", indata, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_as_outresult", result, OracleMappingType.RefCursor, ParameterDirection.Output);
           

            parameters.BindByName = true;
            using var connection = _dapperContext.CreateConnection();
            var response = await connection.QueryAsync<dynamic>
                (procedureName, parameters, commandType: CommandType.StoredProcedure);

            return response;
        }
        public async Task<dynamic> PostKeyEmpData(KeyPunchReqDto keyPunchRequest)
        {
            OracleRefCursor result = null;
           

            var procedureName = "proc_branch_key_post_data";
            var parameters = new OracleDynamicParameters();

            parameters.Add("p_flag", keyPunchRequest.flag, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_indata", keyPunchRequest.indata, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_as_outresult", result, OracleMappingType.RefCursor, ParameterDirection.Output);
           // parameters.Add("p_errorStat", result, OracleMappingType.Int64, ParameterDirection.Output);
           // parameters.Add("p_errorMsg", result, OracleMappingType.Varchar2, ParameterDirection.Output);

            parameters.BindByName = true;
            using var connection = _dapperContext.CreateConnection();

            var response = await connection.QueryAsync<dynamic>(procedureName, parameters, commandType:CommandType.StoredProcedure);
        
     

            return response;
        }


    }
}
