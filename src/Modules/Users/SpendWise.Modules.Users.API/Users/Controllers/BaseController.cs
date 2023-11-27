using Microsoft.AspNetCore.Mvc;
using SpendWise.Shared.Infrastructure.Api;

namespace SpendWise.Modules.Users.API.Users.Controllers;

[ApiController]
[Route("[controller]")]
[ProducesDefaultContentType]
internal abstract class BaseController : ControllerBase
{
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is not null)
        {
            return Ok(model);
        }

        return NotFound();
    }
}