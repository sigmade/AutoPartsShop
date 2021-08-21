using Microsoft.EntityFrameworkCore;
using Sigmade.Application.Reports.Dtos;
using Sigmade.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sigmade.Application.Reports
{
    public class ReportsService
    {
        private readonly ApplicationDbContext _db;

        public ReportsService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<TopGoodsDto>> GetTopGoods(int count)
        {
            return await _db.OrderHistories
                .GroupBy(g => new { g.VendorCode, g.Brand })
                .Select(gt => new TopGoodsDto
                {
                    VendorCode = gt.Key.VendorCode,
                    Count = gt.Count(),
                    Brand = gt.Key.Brand
                })
                .OrderByDescending(c => c.Count)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<CountGoodsDto>> GetNotPurchasedGoods()
        {
            var orderVendorCodes = _db.OrderHistories
                .GroupBy(o => o.VendorCode)
                .Select(or => new CountGoodsDto { VendorCode = or.Key, Count = or.Count() });

            var searchVendorCodes = _db.SearchHistories
                .GroupBy(o => o.VendorCode)
                .Select(or => new CountGoodsDto { VendorCode = or.Key, Count = or.Count() });

            var notPurchasedCodes = searchVendorCodes.Select(x => x.VendorCode)
                .Except(orderVendorCodes.Select(x => x.VendorCode));

            return await searchVendorCodes
                .Where(s => notPurchasedCodes.Contains(s.VendorCode))
                .ToListAsync();
        }
    }
}
