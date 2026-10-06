using Microsoft.AspNetCore.Mvc;

namespace InventarySales.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GreetingController : ControllerBase
    {
        [HttpGet(Name = "GetGreeting")]
        public string Get()
        {
            return "Hello, welcome to the InventarySales API!";
        }
    };
}
