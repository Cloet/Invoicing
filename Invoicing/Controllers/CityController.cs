using Microsoft.AspNetCore.Mvc;

namespace Invoicing.Controllers
{
    public class CityController : Controller
    {
        public async Task<IActionResult> GetCity()
        {
            return NotFound();
        }

    }
}
