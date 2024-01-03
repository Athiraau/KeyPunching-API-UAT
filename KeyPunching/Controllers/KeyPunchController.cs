using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bussiness.Contracts;
using Bussiness.Services;
using System.Reflection.Metadata.Ecma335;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Drawing.Printing;
using DataAccess.Contracts;
using DataAccess.Dto.Request;

namespace KeyPunching.Controllers
{
    // [Authorize]
    [AllowAnonymous]
    [Route("api/keyPunch")]
    [ApiController]
    public class KeyPunchController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IServiceWrapper _service;
        private readonly ServiceHelper _serviceHelper;


        public KeyPunchController(ILoggerService logger, IServiceWrapper service, ServiceHelper serviceHelper)
        {
            _service = service;
            _serviceHelper = serviceHelper;
            _logger = logger;
        }
        [HttpGet("GetKeyEmpData/{flag}/{indata}", Name = "GetKeyEmpData")]
        public async Task<IActionResult> GetKeyEmpData([FromRoute] string flag, string indata)
        {

            //FLAG VALIDATION ------------------------------------------------------- 
            var errorRes = _serviceHelper.VHelper.ValidateKeyPunchData(flag, indata);



            if (errorRes.Result.errorMessage.Count > 0)
            
            {
                _logger.LogError("Invalid/wrong request data  sent from client.");
                
                return BadRequest(errorRes.Result.errorMessage);
            }


            //REPOSITORY METHOD CALLING ------------------------------------------------
            var punchdata = await _service.keyPunchService.GetKeyEmpData(flag, indata); 
             if (punchdata == null) 
            {
                _logger.LogError($"Details of filter data could not be returned in db.");

                return NotFound();

            }
             else
            {
                _logger.LogInfo($"Returned details of data required to load filter for flag: {flag}");

                return Ok(JsonConvert.SerializeObject(punchdata));

            }

        }



        [HttpPost("PostKeyEmpData", Name = "PostKeyEmpData")]
        public async Task<IActionResult> PostKeyEmpData([FromBody] KeyPunchReqDto keypunchreq)
        {
            var errorRes = _serviceHelper.VHelper.ValidateKeyPunchData(keypunchreq);



            if (errorRes.Result.errorMessage.Count > 0)

            {
                _logger.LogError("Invalid/wrong request data  sent from client.");

                return BadRequest(errorRes.Result.errorMessage);
            }

            var punchdata = await _service.keyPunchService.PostKeyEmpData(keypunchreq);
            if (punchdata == null)
            {
                _logger.LogError($"Details of filter data could not be returned in db.");

                return NotFound();

            }
            else
            {
                _logger.LogInfo($"Returned response data after saving early going req: {keypunchreq.flag}");

                return Ok(JsonConvert.SerializeObject(punchdata));

            }

        }

    }
}
