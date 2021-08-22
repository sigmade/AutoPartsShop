using System.Collections.Generic;

namespace Sigmade.Application.Reports.Dtos
{
    public class OrdersDto
    {
        public int TotalCount { get; set; }

        public List<CityCountDto> Cities { get; set; }
    }
}
