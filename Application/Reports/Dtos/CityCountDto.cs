using System.Collections.Generic;

namespace Sigmade.Application.Reports.Dtos
{
    public class CityCountDto
    {
        public string City { get; set; }
        public int OrdersCount { get; set; }

        public List<CompanyCountDto> Companies { get; set; }
    }
}
