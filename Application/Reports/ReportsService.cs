using Microsoft.EntityFrameworkCore;
using Sigmade.Application.Reports.Dtos;
using Sigmade.Domain;
using System;
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

        public async Task<List<TopProductsDto>> GetTopProducts(int count)
        {
            return await _db.OrderHistories
                .GroupBy(g => new { g.VendorCode, g.Brand })
                .Select(gt => new TopProductsDto
                {
                    VendorCode = gt.Key.VendorCode,
                    Count = gt.Count(),
                    Brand = gt.Key.Brand
                })
                .OrderByDescending(c => c.Count)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<CountProductsDto>> GetNotPurchasedProducts()
        {
            var orderVendorCodes = _db.OrderHistories
                .GroupBy(o => new { o.VendorCode, o.Brand })
                .Select(or => new CountProductsDto { VendorCode = or.Key.VendorCode, Brand = or.Key.Brand, Count = or.Count() });

            var searchVendorCodes = _db.SearchHistories
                .GroupBy(o => new { o.VendorCode, o.Brand })
                .Select(or => new CountProductsDto { VendorCode = or.Key.VendorCode, Brand = or.Key.Brand, Count = or.Count() });

            var notPurchasedCodes = searchVendorCodes.Select(x => x.VendorCode)
                .Except(orderVendorCodes.Select(x => x.VendorCode));

            return await searchVendorCodes
                .Where(s => notPurchasedCodes.Contains(s.VendorCode))
                .ToListAsync();
        }

        public async Task<List<ConversionProductsDto>> GetProductsConversion()
        {
            var orderVendorCodes = await _db.OrderHistories
                .GroupBy(o => new { o.VendorCode, o.Brand })
                .Select(or => new { or.Key.VendorCode, or.Key.Brand, Count = or.Count() })
                .ToListAsync();

            var searchVendorCodes = await _db.SearchHistories
                .GroupBy(o => new { o.VendorCode, o.Brand })
                .Select(or => new { or.Key.VendorCode, or.Key.Brand, Count = or.Count() })
                .ToListAsync();

            return (from searches in searchVendorCodes
                    join orders in orderVendorCodes on searches.VendorCode equals orders.VendorCode into un
                    from orders in un.DefaultIfEmpty()
                    select new ConversionProductsDto
                    {
                        VendorCode = searches.VendorCode,
                        Brand = searches.Brand,
                        Ratio = orders != null ? orders.Count / (float)searches.Count : 0

                    }).OrderByDescending(c => c.Ratio).ToList();
        }

        public async Task<OrdersDto> GetOrders()
        {
            var orders = _db.OrderHistories.Include(d => d.UserContragent);

            var byCompany = await orders
                .GroupBy(c => new { c.UserContragent.Name, c.UserContragent.City })
                .Select(d => new CompanyCountDto
                {
                    Name = d.Key.Name,
                    City = d.Key.City,
                    OrdersCount = d.Count()
                })
                .ToListAsync();

            var byCity = byCompany
                .GroupBy(c => c.City)
                .Select(d => new CityCountDto
                {
                    City = d.Key,
                    OrdersCount = d.Count(),
                    Companies = byCompany
                        .Where(c => c.City == d.Key)
                        .ToList()
                })
                .ToList();

            return new()
            {
                TotalCount = byCompany.Sum(c => c.OrdersCount),
                Cities = byCity
            };
        }
    }
}
