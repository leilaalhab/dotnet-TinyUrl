using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.TinyUrl
{
    public class GetTinyUrlDto
    {
        public int Id { get; set; }

        public String? Url { get; set; }

        public int UsagesCount { get; set; }
        public List<GetUsageDto>? Usages { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateExpired { get; set; }
    }
}