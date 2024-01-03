using Bussiness.Contracts;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services.GetPunch
{
    public class KeyPunchService : IKeyPunchService
    {
        


        private readonly KeyPunchGetDetails _keypunchgetdetails;
        private readonly DtoWrapper  _dto;

        public KeyPunchService(KeyPunchGetDetails keypunchgetdetails, DtoWrapper dto)
        {
            _keypunchgetdetails = keypunchgetdetails;
            _dto = dto;
        }
        public async Task<dynamic> GetKeyEmpData(string flag, string indata)
        {

            // METHOD CALLING
            var punchdata = await _keypunchgetdetails.GetKeyEmpData(flag, indata);
           
            _dto.keyPunchRes_dto.KeyPunchData = punchdata;

            return _dto.keyPunchRes_dto;

        }

        public async Task<dynamic> PostKeyEmpData(KeyPunchReqDto keypunchreq)
        {
            var LeaveDate = await _keypunchgetdetails.PostKeyEmpData(keypunchreq);
            _dto.keyPunchRes_dto.KeyPunchData = LeaveDate;

            return _dto.keyPunchRes_dto;
        }

    }
}
