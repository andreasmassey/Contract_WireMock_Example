using System;
using ContractWithWireMock.Services;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ContractWithWireMock.Messages;

namespace ContractWithWireMock.Controllers
{
    [Route("api/decision")]
    [ApiController]
    public class DecisionController : Controller
    {
        private readonly IDecisionService _decisionService;
        private readonly ILogger<DecisionController> _logger;

        public DecisionController(IDecisionService decisionService, ILoggerFactory loggerFactory)
        {
            _decisionService = decisionService;
            _logger = loggerFactory.CreateLogger<DecisionController>();
        }

        [HttpPost("v1")]
        public async Task<IActionResult> MakeDecision([FromBody] BeginDecision.Request request)
        {
            try
            {
                if (request.DecisionId == 0)
                {
                    var errorResponse = new ErrorResponse("DecisionId can not be 0");
                    return BadRequest(errorResponse.Errors);
                }

                //Get Response
                var response = await _decisionService.GetDecision(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, new ErrorResponse(ex).Errors);
            }

        }
    }
}
