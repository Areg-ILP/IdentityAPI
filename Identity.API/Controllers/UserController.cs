using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Queries.QueriesAbstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserQueries _userQueries;

        public UsersController(IUserQueries userQueries)
        {
            _userQueries = userQueries;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SingleAsync(int id)
        {
            var res = await _userQueries.SingleAsync(id);
            return DefineActionResult(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationModel paginationModel)
        {
            var res = await _userQueries.GetAllAsync(paginationModel);
            return DefineActionResult(res);
        }
    }
}
