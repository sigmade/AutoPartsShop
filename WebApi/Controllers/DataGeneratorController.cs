using Microsoft.AspNetCore.Mvc;
using Sigmade.DataGenerator;
using System.Net;
using System.Threading.Tasks;

namespace Sigmade.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataGeneratorController : ControllerBase
    {
        private readonly FakeDataService _fakeDataService;

        public DataGeneratorController(FakeDataService fakeDataService)
        {
            _fakeDataService = fakeDataService;
        }

        [HttpPost("add-users")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> AddFakeUsers(int count)
        {
            await _fakeDataService.AddUser(count);
            return NoContent();
        }

        [HttpPost("add-user-contragent")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> AddFakeUserContragents(int count)
        {
            await _fakeDataService.AddUserContragent(count);
            return NoContent();
        }

        [HttpPost("add-search-history")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> AddSearchHistory(int count)
        {
            var res = await _fakeDataService.AddSearchHistory(count);
            return Ok(res);
        }

        [HttpPost("add-order-history")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> AddOrderHistory(int count)
        {
            await _fakeDataService.AddOrderHistory(count);
            return NoContent();
        }

        [HttpDelete("clear-all-tables")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ClearAllTables()
        {
            await _fakeDataService.ClearAllTables();
            return NoContent();
        }
    }
}
