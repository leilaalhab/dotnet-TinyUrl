using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.Usage
{
    public class AddUsageDto
    {
        public int UrlId { get; set; }
        public RegionClass Region { get; set; } = RegionClass.NA;
    }
}