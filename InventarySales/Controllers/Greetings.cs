using Microsoft.AspNetCore.Mvc;

namespace InventarySales.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GreetingController : ControllerBase
    {
        [HttpGet(Name = "Greeting")]
        public string Get()
        {
            return "Hello, welcome to the InventarySales API!";
        }
    };
}
