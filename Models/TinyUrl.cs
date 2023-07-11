using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class TinyUrl
    {
        public int Id { get; set; }
        public User? User { get; set; }

        public String? Url { get; set; }

        public int UsagesCount { get; set; } = 0;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateExpired { get; set; } = DateTime.MaxValue;

        public List<Usage>? Usages { get; set; }

        public List<QandA>? QandAs { get; set; }
    }
}