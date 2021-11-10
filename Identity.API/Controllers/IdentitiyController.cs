using Identity.Infastructure.Application.Commands.IdentityCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IdentityController : BaseController
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand user)
        {
            var res = await _mediator.Send(user);
            return DefineActionResult(result: res,
                                      postFlag: true,
                                      identityFlag: true);
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationCommand user)
        {
            var res = await _mediator.Send(user);
            return DefineActionResult(result: res,
                                      postFlag: true,
                                      identityFlag: true);
        }
    }
}
