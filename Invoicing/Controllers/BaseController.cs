using Invoicing.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing.Controllers
{
    public class BaseController: ControllerBase
    {

        protected ObjectResult Generate500ServerError(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ValidationError.CreateError(ex.Message));
        }

    }
}
