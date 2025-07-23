using Microsoft.AspNetCore.Mvc;
using Inflow.Data;
using Inflow.Data.DTO.DataRequest;

namespace Inflow.DataService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IDataQueryable _query;

        public DataController(IDataQueryable query)
        {
            _query = query;
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(DeleteDataRequestBody deleteDataRequestBody)
        {
            var affectedRowsCount = await _query.DeleteAsync(deleteDataRequestBody);
            return Ok(affectedRowsCount);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(InsertDataRequestBody insertDataRequestBody)
        {
            var insertedRecordIds = await _query.InsertAsync(insertDataRequestBody);
            return Ok(insertedRecordIds);
        }

        [HttpPost("Select")]
        public async Task<IActionResult> Select([FromBody] SelectDataRequestBody selectDataRequestBody)
        {
            var records = await _query.SelectAsync(selectDataRequestBody);
            return Ok(records);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateDataRequestBody updateDataRequestBody)
        {
            var affectedRowsCount = await _query.UpdateAsync(updateDataRequestBody);
            return Ok(affectedRowsCount);
        }
    }
}
