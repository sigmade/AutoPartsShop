using Microsoft.AspNetCore.Mvc;
using Sigmade.Application.Reports;
using System.Threading.Tasks;

namespace Sigmade.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ReportsService _reportsService;

        public ReportsController(ReportsService reportsService)
        {
            _reportsService = reportsService;
        }

        [HttpGet("top-goods")]
        public async Task<IActionResult> GetTopGoods(int count)
        {
            var topGoods = await _reportsService.GetTopGoods(count);
            return Ok(topGoods);
        }

        [HttpGet("not-purchased-goods")]
        public async Task<IActionResult> GetNotPurchasedGoods()
        {
            var notPurchasedGoods = await _reportsService.GetNotPurchasedGoods();
            return Ok(notPurchasedGoods);
        }
    }
}
