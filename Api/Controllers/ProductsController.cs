using Microsoft.AspNetCore.Mvc;

namespace SriDurgaHariHaraBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() => Ok("Products endpoint working");
    }
}