using Identity.Infastructure.Application.Commands.RoleCommands;
using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Queries.QueriesAbstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : BaseController
    {
        private readonly IRoleQueries _roleQueries;
        private readonly IMediator _mediator;
        
        public RoleController(IRoleQueries roleQueries, IMediator mediator)
        {
            _roleQueries = roleQueries;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SingleAsync(int id)
        {
            var res = await _roleQueries.SingleAsync(id);
            return DefineActionResult(res, false);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationModel paginationModel)
        {
            var res = await _roleQueries.GetAllAsync(paginationModel);
            return DefineActionResult(res, false);
        }

        [HttpPost("add-role")]
        public async Task<IActionResult> AddRoleAsync(AddRoleCommand model)
        {
            var res = await _mediator.Send(model);
            return DefineActionResult(res, true);
        }

        [HttpDelete("delete-role")]
        public async Task<IActionResult> DeleteRoleAsync(DeleteRoleCommand model)
        {
            var res = await _mediator.Send(model);
            return DefineActionResult(res, true);
        }

        [HttpPut("update-role")]
        public async Task<IActionResult> UpdateRoleAsync(UpdateRoleCommand model)
        {
            var res = await _mediator.Send(model);
            return DefineActionResult(res, true);
        }
    }
}
