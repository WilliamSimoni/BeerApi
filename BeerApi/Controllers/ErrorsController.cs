using Microsoft.AspNetCore.Mvc;

namespace BeerApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            //return Ok();
            return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An internal error has occurred");
        }
    }
}
