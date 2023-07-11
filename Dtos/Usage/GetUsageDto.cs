using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.Usage
{
    public class GetUsageDto
    {
        public DateTime UseTime { get; set; } = DateTime.Now;
        public RegionClass Region { get; set; } = RegionClass.NA;
    }
}