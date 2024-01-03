using Bussiness.Contracts;
using Bussiness.Services.GetPunch;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services
{
    public class ServiceWrapper : IServiceWrapper
    {
        private IKeyPunchService _keypunchservice;
        private readonly IConfiguration _config;
        private readonly DtoWrapper _dto;
        private readonly KeyPunchGetDetails _keypunchgetdetails;
        private IJwtUtils _jwtUtils;
        private readonly ILoggerService _logger;


        public ServiceWrapper(ILoggerService logger, IKeyPunchService keypunchservice, IConfiguration config, DtoWrapper dto,KeyPunchGetDetails keypunchgetdetails)
        {
            _keypunchservice = keypunchservice;
            _config = config;
            _dto = dto;
            _keypunchgetdetails = keypunchgetdetails;
            _logger = logger;
        }

        public IJwtUtils JwtUtils
        {
            get
            {
                if (_jwtUtils == null)
                {
                    _jwtUtils = new JwtUtils(_config, _logger);
                }
                return _jwtUtils;
            }
        }
        public IKeyPunchService keyPunchService
        {
            get
            {
                if (_keypunchservice == null)
                {
                    //  _keypunchservice = new keypunch(_keypunchgetdetails, _dto);

                    _keypunchservice = new KeyPunchService(_keypunchgetdetails, _dto);
                }
                return _keypunchservice;
            }
        }
    }
}
