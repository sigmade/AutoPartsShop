// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
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
