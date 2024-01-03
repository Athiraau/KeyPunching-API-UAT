using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Entities;
using FluentValidation;

namespace Bussiness.Helpers
{
   public class ValidationHelper
    {
        private readonly ErrorResponse _error;
        private readonly DtoWrapper _dto;
        //private readonly IValidator<KeyPunchReqDto> _keypunchreqValidator;
        private readonly IValidator<KeyPunchReqDetailsDto> _keypunchreqDetailsValidator;
        private readonly IValidator<KeyPunchEmpGetDto> _keypunchempGetValidator;

        

        public ValidationHelper(ErrorResponse error, DtoWrapper dto, IValidator<KeyPunchEmpGetDto> keypunchempGetValidator, IValidator<KeyPunchReqDetailsDto> keypunchreqDetailsValidator)
        {
           _error = error;
            _dto = dto;
            _keypunchempGetValidator = keypunchempGetValidator;
            _keypunchreqDetailsValidator = keypunchreqDetailsValidator;
            
        }

        public async Task<ErrorResponse> ValidateKeyPunchData(string flag, string indata)
        {            
            ErrorResponse errorRes = null;

//DISABLED EMPCODE VALIDATION -----------------------------------------------------------------

            //  String[] strlist = Convert.ToString(indata).Split("~", StringSplitOptions.RemoveEmptyEntries);

            /*    if (flag == "GETFKEYDETAILS")
                { _dto.KeyPunchEmpGet.emp_code = strlist[1];
                    var validationResult = await _keypunchempGetValidator.ValidateAsync(_dto.KeyPunchEmpGet);
                    errorRes = ReturnErrorRes(validationResult);
                }
                if (flag == "GETSKEYDETAILS")
                {
                    _dto.KeyPunchEmpGet.emp_code = strlist[2];
                    var validationResult = await _keypunchempGetValidator.ValidateAsync(_dto.KeyPunchEmpGet);
                    errorRes = ReturnErrorRes(validationResult);
                }
               

                */

//--------------------------------------------------------------------------------------------------------


            //ONLY FLAG VALIDATION -----------------------

            _dto.KeyPunchEmpGet.p_flag = flag;
                var validationResult = await _keypunchempGetValidator.ValidateAsync(_dto.KeyPunchEmpGet);
                errorRes = ReturnErrorRes(validationResult);

          //  }
            return errorRes;
        }

        public async Task<ErrorResponse> ValidateKeyPunchData(KeyPunchReqDto keypunchReq)
        {
            ErrorResponse errorRes = null;
            String[] strlist = Convert.ToString(keypunchReq.indata).Split("~"); //, StringSplitOptions.RemoveEmptyEntries);


            var flag = keypunchReq.flag;
            var indata = keypunchReq.indata;

//-----------------------------------------------------------------------------------------------
         /*   if (flag == "BRANCHKEYCONFIRM")
            {
                _dto.keyPunchReqDetailsDto.brid = strlist[0] == null || strlist[0] == string.Empty ? string.Empty : strlist[0];
                _dto.keyPunchReqDetailsDto.fempid = strlist[1] == null || strlist[0] == string.Empty ? string.Empty : strlist[0];
                _dto.keyPunchReqDetailsDto.sempid = strlist[2] == null || strlist[0] == string.Empty ? string.Empty : strlist[0];

                var validationResult = await _keypunchreqDetailsValidator.ValidateAsync(_dto.keyPunchReqDetailsDto);
                errorRes = ReturnErrorRes(validationResult);
            }

            //if (flag== "BRKEYREQUEST")
            //{
            //    _dto.keyPunchReqDetailsDto.brid = strlist[0] == null || strlist[0] == string.Empty ? string.Empty : strlist[0];
            //    _dto.keyPunchReqDetailsDto.fempid = strlist[1] == null || strlist[0] == string.Empty ? string.Empty : strlist[0];
            //    _dto.keyPunchReqDetailsDto.sempid = strlist[2] == null || strlist[0] == string.Empty ? string.Empty : strlist[0];

            //    var validationResult = await _keypunchreqDetailsValidator.ValidateAsync(_dto.keyPunchReqDetailsDto);
            //    errorRes = ReturnErrorRes(validationResult);
            //}
            else
            {

            */
//-----------------------------------------------------------------------------------------------------

            //ONLY FLAG VALIDATION

                _dto.KeyPunchEmpGet.p_flag = flag;
                var validationResult = await _keypunchempGetValidator.ValidateAsync(_dto.KeyPunchEmpGet);
                errorRes = ReturnErrorRes(validationResult);
          //  }
           
            return errorRes;
        
        }

        public ErrorResponse ReturnErrorRes(FluentValidation.Results.ValidationResult Res)
        {
            List<string> errors = new List<string>();
            foreach (var row in Res.Errors.ToArray())
            {
                errors.Add(row.ErrorMessage.ToString());
            }
            _error.errorMessage = errors;
            return _error;
        }
    }
}
