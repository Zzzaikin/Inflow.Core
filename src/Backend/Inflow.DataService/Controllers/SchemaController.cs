using Microsoft.AspNetCore.Mvc;
using Inflow.Data.Schema;

namespace Inflow.DataService.Controllers;

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