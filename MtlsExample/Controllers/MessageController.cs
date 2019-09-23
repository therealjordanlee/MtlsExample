using Microsoft.AspNetCore.Mvc;

namespace MtlsExample.Controllers
{
    [Route("")]
    public class MessageController : Controller
    {
        [HttpGet]
        public IActionResult GetMessage()
        {
            return Ok("Super Secret Message");
        }
    }
}
