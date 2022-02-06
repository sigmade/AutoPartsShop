﻿// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace Sigmade.Application.Reports.Dtos
{
    public class CountProductsDto
    {
        public string VendorCode { get; set; }

        public string Brand { get; set; }
        public int Count { get; set; }
    }
}
