using Identity.Infastructure.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    public class BaseController : Controller
    {
        [NonAction]
        protected IActionResult DefineActionResult<T>(ResultModel<T> result, bool postFlag = false, bool identityFlag = false)
        {
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                if (identityFlag)
                {
                    return Unauthorized();
                }
                if (postFlag)
                {
                    return ValidationProblem();
                }
            }

            return NotFound();
        }

        [NonAction]
        protected int GetUserId()
        {
            int.TryParse(User.Claims.ToList().FirstOrDefault(e => e.Type == "Id")?.Value, out int userId);
            return userId;
        }
    }
}
