using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.TinyUrl
{
    public class UpdatedTinyUrlDto
    {
        public int Id { get; set; }

        public DateTime DateExpired { get; set; }

    }
}