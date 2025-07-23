using Microsoft.AspNetCore.Mvc;
using Inflow.Data.Schema;

namespace Inflow.DataService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchemaController : ControllerBase
    {
        private readonly ISchema _schema;

        public SchemaController(ISchema schema) 
        {
            _schema = schema;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string name)
        {
            await _schema.GetAsync(name);
            return Ok();
        }
    }
}
