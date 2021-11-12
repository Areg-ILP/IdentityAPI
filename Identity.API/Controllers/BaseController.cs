using Identity.Infastructure.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Identity.API.Controllers
{
    public class BaseController : Controller
    {
        [NonAction]
        protected IActionResult DefineActionResult<T>(ResultModel<T> result,
                                                        bool postFlag = false,
                                                        bool identityFlag = false)
        {
            if(result != null)
            {
                if(result.IsSuccessed)
                {
                    return Ok(result);
                }
                else
                {
                    if (postFlag)
                    {
                        return ValidationProblem(result.Message);
                    }
                    if (identityFlag)
                    {
                        return Unauthorized();
                    }
                }
            }
            return NotFound(result.Message);
        }

        [NonAction]
        protected int GetUserId()
        {
            int.TryParse(User.Claims.ToList()
                                    .FirstOrDefault(e => e.Type == "Id")?.Value, out int userId);
            return userId;
        }
    }
}
