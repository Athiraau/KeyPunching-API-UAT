using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Contracts
{
    public interface IKeyPunchService
    {
        public Task<dynamic> GetKeyEmpData(string flag, string indata);
        public Task<dynamic> PostKeyEmpData(KeyPunchReqDto keypunchreq);

    }
}
