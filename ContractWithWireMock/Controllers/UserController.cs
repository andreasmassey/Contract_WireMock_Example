using System;
using System.Threading.Tasks;
using Contracts;
using ContractWithWireMock.Messages;
using ContractWithWireMock.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContractWithWireMock.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILoggerFactory loggerFactory)
        {
            _userService = userService;
            _logger = loggerFactory.CreateLogger<UserController>();
        }

        [HttpPost("v1")]
        public async Task<IActionResult> Getuser([FromBody] GetUser.Request request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.UserId))
                {
                    var errorResponse = new ErrorResponse("UserId can not be 0");
                    return BadRequest(errorResponse.Errors);
                }

                //Get Response
                var response = await _userService.GetUser(request);

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