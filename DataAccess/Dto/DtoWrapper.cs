using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto
{
    public class DtoWrapper
    {
        private KeyPunchReqDto _keyPunchReq_dto;
        private KeyPunchResDto _keyPunchRes_dto;
        private KeyPunchReqDetailsDto _keyPunchReqDetails_dto;
        private KeyPunchEmpGetDto _KeyPunchEmpGetDto;


        public KeyPunchReqDetailsDto keyPunchReqDetailsDto
        {
            get
            {
                if (_keyPunchReqDetails_dto == null)
                {
                    _keyPunchReqDetails_dto = new KeyPunchReqDetailsDto();
                }
                return _keyPunchReqDetails_dto;
            }


        }
        public KeyPunchReqDto keyPunchReq_dto
        {
            get
            {
                if (_keyPunchReq_dto == null)
                {
                    _keyPunchReq_dto = new KeyPunchReqDto();
                }
                return _keyPunchReq_dto;
            }


        }

        public KeyPunchResDto keyPunchRes_dto
        {
            get
            {
                if (_keyPunchRes_dto == null)
                {
                    _keyPunchRes_dto = new KeyPunchResDto();
                }
                return _keyPunchRes_dto;
            }

                    }

        public KeyPunchEmpGetDto KeyPunchEmpGet
        {
            get
            {
                if (_KeyPunchEmpGetDto == null)
                {
                    _KeyPunchEmpGetDto = new KeyPunchEmpGetDto();
                }
                return _KeyPunchEmpGetDto;
            }
        }
    }
}
