using Bussiness.Helpers;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services
{
    public class ServiceHelper
    {
        private ValidationHelper _vHelper;
        private readonly ErrorResponse _error;
        private readonly DtoWrapper _dto;
        private readonly IValidator<KeyPunchEmpGetDto> _keypunchGetvalidator;
        private readonly IValidator<KeyPunchReqDetailsDto> _keypunchPostvalidator;

        public ServiceHelper(ErrorResponse error,DtoWrapper dto, IValidator<KeyPunchEmpGetDto> keypunchGetvalidator, IValidator<KeyPunchReqDetailsDto> keypunchPostvalidator)
        {
            _error = error;
            _dto = dto;
            _keypunchGetvalidator = keypunchGetvalidator;
            _keypunchPostvalidator = keypunchPostvalidator;
        }
        public ValidationHelper VHelper
        {
            get
            {
                if (_vHelper == null)
                {
                    _vHelper = new ValidationHelper(_error, _dto, _keypunchGetvalidator , _keypunchPostvalidator);
                }
                return _vHelper;
            }
        }



    }
}
