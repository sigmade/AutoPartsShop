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
    }
}
