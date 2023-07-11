using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class Usage
    {
        public TinyUrl? tinyUrl { get; set; }
        public int UsageId { get; set; }
        public DateTime UseTime { get; set; } = DateTime.Now;
        public RegionClass Region { get; set; } = RegionClass.NA;

    }
}