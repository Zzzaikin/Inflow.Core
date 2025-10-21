using Microsoft.AspNetCore.Mvc;
using Inflow.Core.Data.Schema;

namespace Inflow.Core.DataService.Controllers;

[ApiController]
[Route("[controller]")]
public class SchemaController(ISchema schema) : ControllerBase
{
    [HttpGet("Get")]
    public async Task<IActionResult> Get(string name)
    {
        await schema.GetAsync(name);
        return Ok();
    }
}