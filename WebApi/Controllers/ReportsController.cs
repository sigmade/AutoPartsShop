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

        [HttpGet("top-products")]
        public async Task<IActionResult> GetTopGoods(int count)
        {
            var topGoods = await _reportsService.GetTopProducts(count);
            return Ok(topGoods);
        }

        [HttpGet("not-purchased-products")]
        public async Task<IActionResult> GetNotPurchasedGoods()
        {
            var notPurchasedGoods = await _reportsService.GetNotPurchasedProducts();
            return Ok(notPurchasedGoods);
        }

        [HttpGet("conversion-products")]
        public async Task<IActionResult> GetProcuctConversion()
        {
            var productConversion = await _reportsService.GetProductsConversion();
            return Ok(productConversion);
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders()
        {
            var s = await _reportsService.GetOrders();
            return Ok(s);
        }
    }
}
