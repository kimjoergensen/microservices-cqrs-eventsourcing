using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion("1")]
        public ActionResult Get() {
            return Ok();
        }
    }
}