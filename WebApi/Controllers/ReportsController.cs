using Microsoft.AspNetCore.Mvc;
using Sigmade.Application.Reports;
using Sigmade.Application.Reports.Dtos;
using System.Net;
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TopProductsDto))]
        public async Task<IActionResult> GetTopGoods(int count)
        {
            var topGoods = await _reportsService.GetTopProducts(count);
            return Ok(topGoods);
        }

        [HttpGet("not-purchased-products")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CountProductsDto))]
        public async Task<IActionResult> GetNotPurchasedGoods()
        {
            var notPurchasedGoods = await _reportsService.GetNotPurchasedProducts();
            return Ok(notPurchasedGoods);
        }

        [HttpGet("conversion-products")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ConversionProductsDto))]
        public async Task<IActionResult> GetProcuctConversion()
        {
            var productConversion = await _reportsService.GetProductsConversion();
            return Ok(productConversion);
        }

        [HttpGet("orders")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(OrdersDto))]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _reportsService.GetOrders();
            return Ok(orders);
        }
    }
}
