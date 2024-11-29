using Microsoft.AspNetCore.Mvc;

namespace Store.Controllers
{
    [Route("")]
    [ApiController]
    public class WelcomeController : BaseApiController
    {
            [HttpGet]
            public IActionResult DefaultRoute()
            {
                return Ok("Welcome to the Blend Store API!");
            }
    }
}
